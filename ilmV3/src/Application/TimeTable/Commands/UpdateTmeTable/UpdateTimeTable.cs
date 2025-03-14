using ilmV3.Application.TimeTable.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Commands.UpdateTmeTable;
public record UpdateTimeTableCommand(int timeTableId, TimeTableDto timeTable) : IRequest<TimeTableVM?>;

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

        timeTable.Audience = request.timeTable.Audience;
        timeTable.StudentGroupId = request.timeTable.StudentGroupId;
        timeTable.SubjectId = request.timeTable.SubjectId;
        timeTable.TeacherId = request.timeTable.TeacherId;
        timeTable.WeekDay = request.timeTable.WeekDay;
        timeTable.Date = request.timeTable.Date;
        timeTable.Time = request.timeTable.Time;
        timeTable.Name = request.timeTable.Name;

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
