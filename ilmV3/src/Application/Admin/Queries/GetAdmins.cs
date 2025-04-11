using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Admin.Queries;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public class GetAdminsQuery : IRequest<IEnumerable<AdminVM>>;

public class GetAdminsQueryHandler : IRequestHandler<GetAdminsQuery, IEnumerable<AdminVM>>
{
    private readonly IAdminRepository _adminRepository;

    public GetAdminsQueryHandler(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }
    public async Task<IEnumerable<AdminVM>> Handle(GetAdminsQuery request, CancellationToken cancellationToken)
    {
        var admins = await _adminRepository.GetAdminsAsync();

        List<AdminVM> result = new List<AdminVM>();
        foreach (var admin in admins)
        {
            AdminVM adminVM = new AdminVM
            {
                Id = admin.Id,
                Name = admin.Name,
            };
            result.Add(adminVM);
        }
        return result;
    }
}
