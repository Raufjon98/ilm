using ilmV3.Application.Common.Security;
using ilmV3.Application.TimeTable.Queries;
using ilmV3.Domain.Constants;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Commands.CreateTimeTable;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record CreateTimeTableCommand(TimeTableDto timeTable) : IRequest<TimeTableVM>;

public class CreateTimeTableCommandHandler : IRequestHandler<CreateTimeTableCommand, TimeTableVM>
{
    private readonly ITimeTableRepository _timeTableRepository;
    public CreateTimeTableCommandHandler(ITimeTableRepository timeTableRepository, IMapper mapper)
    {
        _timeTableRepository = timeTableRepository;
    }
    public async Task<TimeTableVM> Handle(CreateTimeTableCommand request, CancellationToken cancellationToken)
    {
        TimeTableEntity timeTable = new TimeTableEntity()
        {
            Name = request.timeTable.Name,
            StudentGroupId = request.timeTable.StudentGroupId,
            TeacherId = request.timeTable.TeacherId,
            SubjectId = request.timeTable.SubjectId,
            Audience = request.timeTable.Audience,
            Date = request.timeTable.Date,
            Time = request.timeTable.Time,
            WeekDay = request.timeTable.WeekDay,
        };
        var result = await _timeTableRepository.CreateTimeTableAsync(timeTable, cancellationToken);

        TimeTableVM TimeTableVM = new TimeTableVM
        {
            Id = result.Id,
            Name = request.timeTable.Name,
            StudentGroupId = timeTable.StudentGroupId,
            TeacherId = result.TeacherId,
            SubjectId = result.SubjectId,
            Audience = result.Audience,
            Date = result.Date,
            Time = result.Time,
            WeekDay = result.WeekDay,
        };
        return TimeTableVM;
    }
}
