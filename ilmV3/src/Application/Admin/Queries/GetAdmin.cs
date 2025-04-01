using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Admin.Queries;
public record GetAdminQuery(string adminId) : IRequest<AdminEntity>;

public class GetAdminQueryHandler : IRequestHandler<GetAdminQuery, AdminEntity>
{
    private readonly IIdentityService _identityService;
    private readonly IAdminRepository _adminRepository;

    public GetAdminQueryHandler(IIdentityService identityService, IAdminRepository adminRepository)
    {
        _identityService = identityService;
        _adminRepository = adminRepository;
    }
    public async Task<AdminEntity> Handle(GetAdminQuery request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserByIdAsync(request.adminId);
        ArgumentNullException.ThrowIfNull(user);

        var admin = await _adminRepository.GetAdminAsync(user.ExternalUserId);
        ArgumentNullException.ThrowIfNull(admin);
        return admin;
    }
}
