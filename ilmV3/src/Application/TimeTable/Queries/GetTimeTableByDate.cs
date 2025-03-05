using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Queries;
public record GetTimeTableByDateQuery(DateOnly date) : IRequest<TimeTableVM>;

public class GetTimeTableByDateQueryHandler : IRequestHandler<GetTimeTableByDateQuery, TimeTableVM>
{
    private readonly ITimeTableRepository _timeTableRepository;
    private readonly IMapper _mapper;
    public GetTimeTableByDateQueryHandler(ITimeTableRepository timeTableRepository, IMapper mapper)
    {
         _timeTableRepository = timeTableRepository;
        _mapper = mapper;
    }
    public async Task<TimeTableVM> Handle(GetTimeTableByDateQuery request, CancellationToken cancellationToken)
    {
        var result = await _timeTableRepository.GetTimeTableByDateAsync(request.date);
        if (result == null)
        {
            throw new Exception("The Null Error!");
        }
        return _mapper.Map<TimeTableVM>(result);
    }
}
