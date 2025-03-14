using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Queries;
public record GetTimeTablesQuery : IRequest<IEnumerable<TimeTableVM>>;

public class GetTimeTablesQueryHandler : IRequestHandler<GetTimeTablesQuery, IEnumerable<TimeTableVM>>
{
    private readonly ITimeTableRepository _timeTableRepository;
    public GetTimeTablesQueryHandler(ITimeTableRepository timeTableRepository, IMapper mapper)
    {
        _timeTableRepository = timeTableRepository;
    }
    public async Task<IEnumerable<TimeTableVM>> Handle(GetTimeTablesQuery request, CancellationToken cancellationToken)
    {
        var timeTables = await _timeTableRepository.GetTimeTablesAsync();

        List<TimeTableVM> result = new List<TimeTableVM>();
        foreach (var timeTable in timeTables)
        {
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
            result.Add(timeTableVM);
        }
        return result;
    }
}
