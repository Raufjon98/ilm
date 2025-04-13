using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Admin.Queries;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record GetAdminQuery(string adminId) : IRequest<AdminVM>;

public class GetAdminQueryHandler : IRequestHandler<GetAdminQuery, AdminVM>
{
    private readonly IIdentityService _identityService;
    private readonly IAdminRepository _adminRepository;

    public GetAdminQueryHandler(IIdentityService identityService, IAdminRepository adminRepository)
    {
        _identityService = identityService;
        _adminRepository = adminRepository;
    }
    public async Task<AdminVM> Handle(GetAdminQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserByIdAsync(request.adminId);
        ArgumentNullException.ThrowIfNull(user);

        var admin = await _adminRepository.GetAdminAsync(user.ExternalUserId);
        ArgumentNullException.ThrowIfNull(admin);

        AdminVM adminVM = new AdminVM
        {
            Id = admin.Id,
            Name = admin.Name,
        };
        return adminVM;
    }
}
