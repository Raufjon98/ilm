using ilmV3.Application.Common.Security;
using ilmV3.Application.TimeTable.Queries;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Commands.UpdateTmeTable;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record UpdateTimeTableCommand(int timeTableId, TimeTableDto TimeTable) : IRequest<TimeTableVM?>;

public class UpdateTimeTableCommandHandler : IRequestHandler<UpdateTimeTableCommand, TimeTableVM?>
{
    private readonly ITimeTableRepository _timeTableRepository;
    public UpdateTimeTableCommandHandler(ITimeTableRepository timeTableRepository)
    {
        _timeTableRepository = timeTableRepository;
    }
    public async Task<TimeTableVM?> Handle(UpdateTimeTableCommand request, CancellationToken cancellationToken)
    {
        var timeTable = await _timeTableRepository.GetTimeTableByIdAsync(request.timeTableId);
        if (timeTable == null)
        {
            return null;
        }

        timeTable.Audience = request.TimeTable.Audience;
        timeTable.StudentGroupId = request.TimeTable.StudentGroupId;
        timeTable.SubjectId = request.TimeTable.SubjectId;
        timeTable.TeacherId = request.TimeTable.TeacherId;
        timeTable.WeekDay = request.TimeTable.WeekDay;
        timeTable.Date = request.TimeTable.Date;
        timeTable.Time = request.TimeTable.Time;
        timeTable.Name = request.TimeTable.Name;

        var result = await _timeTableRepository.UpdateTimeTableAsync(timeTable, cancellationToken);

        TimeTableVM timeTableVM = new TimeTableVM
        {
            Id = result.Id,
            Name = result.Name,
            StudentGroupId = result.StudentGroupId,
            SubjectId = result.SubjectId,
            TeacherId = result.TeacherId,
            Audience = result.Audience,
            Date = result.Date,
            Time = result.Time,
            WeekDay = result.WeekDay,
        };
        return timeTableVM;
    }
}
