using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Queries;
public record GetTimeTableQuery(int timeTableId) : IRequest<TimeTableVM>;

public class GetTimeTableQueryHandler : IRequestHandler<GetTimeTableQuery, TimeTableVM>
{
    private readonly ITimeTableRepository _timeTableRepository;
    private readonly IMapper _mapper;
    public GetTimeTableQueryHandler(ITimeTableRepository timeTableRepository, IMapper mapper)
    {
        _timeTableRepository = timeTableRepository;
        _mapper = mapper;
    }
    public async Task<TimeTableVM> Handle(GetTimeTableQuery request, CancellationToken cancellationToken)
    {
        var timeTable = await _timeTableRepository.GetTimeTableByIdAsync(request.timeTableId);
        if (timeTable == null)
        {
            throw new KeyNotFoundException($"Record with Id {request.timeTableId} not found!");
        }
        return _mapper.Map<TimeTableVM>(timeTable);
    }
}
