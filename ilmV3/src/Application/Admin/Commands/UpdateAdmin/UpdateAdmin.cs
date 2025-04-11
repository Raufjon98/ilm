using ilmV3.Application.Admin.Queries;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Admin.Commands.UpdateAdmin;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record UpdateAdminCommand(string adminId, AdminDto Admin) : IRequest<AdminEntity>;

public class UpdateAdminCommandHandler : IRequestHandler<UpdateAdminCommand, AdminEntity>
{
    private readonly IIdentityService _identityService;
    private readonly IAdminRepository _adminRepository;

    public UpdateAdminCommandHandler(IIdentityService identityService, IAdminRepository adminRepository)
    {
        _identityService = identityService;
        _adminRepository = adminRepository;
    }

    public async Task<AdminEntity> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserByIdAsync(request.adminId);
        ArgumentNullException.ThrowIfNull(user);

        user.UserName = request.Admin.Name;

        await _identityService.UpdateUserAsync(user);

        var admin = await _adminRepository.GetAdminByIdAsync(user.ExternalUserId);
        ArgumentNullException.ThrowIfNull(admin);

        admin.Name = request.Admin.Name;
        var adminResult = await _adminRepository.UpdateAdminAsync(admin, cancellationToken);

        return adminResult;
    }
}

