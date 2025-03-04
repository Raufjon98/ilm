using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Absent.Queries.GetAbsent;
public record GetAbsentsQuery : IRequest<IEnumerable<AbsentVM>>;

public class GetAbsentQueryHandler : IRequestHandler<GetAbsentsQuery, IEnumerable<AbsentVM>>
{
    private readonly IAbsentRepository _absentRepository;
    private readonly IMapper _mapper;
    public GetAbsentQueryHandler(IMapper mapper, IAbsentRepository absentRepository)
    {
        _mapper = mapper;
        _absentRepository = absentRepository;
    }
    public async Task<IEnumerable<AbsentVM>> Handle(GetAbsentsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<AbsentVM>>( await _absentRepository.GetAbsentsAsync());
    }
}
