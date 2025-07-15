using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.TimeTable.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetTimeTablesQuery : IRequest<IEnumerable<TimeTableVM>>;

public class GetTimeTablesQueryHandler : IRequestHandler<GetTimeTablesQuery, IEnumerable<TimeTableVM>>
{
    private readonly IAplicationDbContext _context;
    public GetTimeTablesQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TimeTableVM>> Handle(GetTimeTablesQuery request, CancellationToken cancellationToken)
    {
        var timeTables = await _context.TimeTables.ToListAsync();

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
