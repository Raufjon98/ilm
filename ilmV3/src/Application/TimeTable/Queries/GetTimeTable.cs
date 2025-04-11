using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetTimeTableQuery(int timeTableId) : IRequest<TimeTableVM>;

public class GetTimeTableQueryHandler : IRequestHandler<GetTimeTableQuery, TimeTableVM>
{
    private readonly ITimeTableRepository _timeTableRepository;
    public GetTimeTableQueryHandler(ITimeTableRepository timeTableRepository, IMapper mapper)
    {
        _timeTableRepository = timeTableRepository;
    }
    public async Task<TimeTableVM> Handle(GetTimeTableQuery request, CancellationToken cancellationToken)
    {
        var timeTable = await _timeTableRepository.GetTimeTableByIdAsync(request.timeTableId);
        if (timeTable == null)
        {
            throw new KeyNotFoundException($"Record with Id {request.timeTableId} not found!");
        }

        TimeTableVM timeTableVM = new TimeTableVM()
        {
            Id = timeTable.Id,
            Name = timeTable.Name,
            StudentGroupId = timeTable.StudentGroupId,
            SubjectId = timeTable.SubjectId,
            TeacherId = timeTable.TeacherId,
            Audience = timeTable.Audience,
            Date = timeTable.Date,
            WeekDay = timeTable.WeekDay,
            Time = timeTable.Time,
        };
        return timeTableVM;
    }
}
