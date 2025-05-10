using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.TimeTable.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetTimeTableByDateQuery(DateOnly date) : IRequest<TimeTableVM>;

public class GetTimeTableByDateQueryHandler : IRequestHandler<GetTimeTableByDateQuery, TimeTableVM>
{
    private readonly IApplicationDbContext _context;
    public GetTimeTableByDateQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<TimeTableVM> Handle(GetTimeTableByDateQuery request, CancellationToken cancellationToken)
    {
        var timeTable = await _context.TimeTables.FirstOrDefaultAsync(time => time.Date == request.date);
        ArgumentNullException.ThrowIfNull(timeTable);
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
