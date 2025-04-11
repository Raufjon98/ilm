using ilmV3.Application.Common.Security;
using ilmV3.Application.TimeTable.Queries;
using ilmV3.Domain.Constants;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Commands.CreateTimeTable;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record CreateTimeTableCommand(TimeTableDto TimeTable) : IRequest<TimeTableVM>;

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
            Name = request.TimeTable.Name,
            StudentGroupId = request.TimeTable.StudentGroupId,
            TeacherId = request.TimeTable.TeacherId,
            SubjectId = request.TimeTable.SubjectId,
            Audience = request.TimeTable.Audience,
            Date = request.TimeTable.Date,
            Time = request.TimeTable.Time,
            WeekDay = request.TimeTable.WeekDay,
        };
        var result = await _timeTableRepository.CreateTimeTableAsync(timeTable, cancellationToken);

        TimeTableVM TimeTableVM = new TimeTableVM
        {
            Id = result.Id,
            Name = request.TimeTable.Name,
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
