using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Admin.Queries;
public class GetAdminsQuery : IRequest<IEnumerable<AdminEntity>>;

public class GetAdminsQueryHandler : IRequestHandler<GetAdminsQuery, IEnumerable<AdminEntity>>
{
    private readonly IAdminRepository _adminRepository;

    public GetAdminsQueryHandler(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }
    public async Task<IEnumerable<AdminEntity>> Handle(GetAdminsQuery request, CancellationToken cancellationToken)
    {
        return await _adminRepository.GetAdminsAsync();
    }
}
