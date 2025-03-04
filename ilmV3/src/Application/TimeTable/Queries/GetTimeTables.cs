using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Queries;
public record GetTimeTablesQuery : IRequest<IEnumerable<TimeTableVM>>;

public class GetTimeTablesQueryHandler : IRequestHandler<GetTimeTablesQuery, IEnumerable<TimeTableVM>>
{
    private readonly ITimeTableRepository _timeTableRepository;
    private readonly IMapper _mapper;
    public GetTimeTablesQueryHandler(ITimeTableRepository timeTableRepository, IMapper mapper)
    {
        _timeTableRepository = timeTableRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<TimeTableVM>> Handle(GetTimeTablesQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<TimeTableVM>>(await _timeTableRepository.GetTimeTablesAsync());
    }
}
