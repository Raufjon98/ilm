using System.Security.Principal;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Admin.Commands.DeleteAdmin;
public record DeleteAdminCommand(string adminId) : IRequest<AdminEntity>;

public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, AdminEntity>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IIdentityService _identityService;

    public DeleteAdminCommandHandler(IIdentityService identityService, IAdminRepository adminRepository)
    {
        _identityService = identityService;
        _adminRepository = adminRepository;
    }
    public async Task<AdminEntity> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserByIdAsync(request.adminId);
        ArgumentNullException.ThrowIfNull(user);

        var admin = await _adminRepository.GetAdminByIdAsync(user.ExternalUserId);
        ArgumentNullException.ThrowIfNull(admin);

        await _identityService.DeleteUserAsync(user.Id);
        var result = await _adminRepository.DeleteAdminAsync(admin, cancellationToken);
        return admin;
    }
}
