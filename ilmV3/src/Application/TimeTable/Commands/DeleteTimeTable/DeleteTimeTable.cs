using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Commands.DeleteTimeTable;
public record DeleteTimeTableCommand(int timeTableId) : IRequest<bool>;

public class DeleteTimeTableCommandHandler : IRequestHandler<DeleteTimeTableCommand, bool>
{
    private readonly ITimeTableRepository _timeTableRepository;
    public DeleteTimeTableCommandHandler(ITimeTableRepository timeTableRepository)
    {
        _timeTableRepository = timeTableRepository;
    }
    public async Task<bool> Handle(DeleteTimeTableCommand request, CancellationToken cancellationToken)
    {
        var timetable = await _timeTableRepository.GetTimeTableByIdAsync(request.timeTableId);
        if (timetable == null)
        {
            throw new KeyNotFoundException($"Record with Id {request.timeTableId} not found");
        }
        return await _timeTableRepository.DeleteTimeTableAsync(timetable, cancellationToken);
    }
}
