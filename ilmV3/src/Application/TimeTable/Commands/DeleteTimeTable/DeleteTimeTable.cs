using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Commands.DeleteTimeTable;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
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
        ArgumentNullException.ThrowIfNull(timetable);
        return await _timeTableRepository.DeleteTimeTableAsync(timetable, cancellationToken);
    }
}
