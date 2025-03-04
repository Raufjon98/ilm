using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.TimeTable.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Commands.UpdateTmeTable;
public record UpdateTimeTableCommand(int timeTableId, TimeTableDto timeTable) : IRequest<bool>;

public class UpdateTimeTableCommandHandler : IRequestHandler<UpdateTimeTableCommand, bool>
{
    private readonly ITimeTableRepository _timeTableRepository;
    public UpdateTimeTableCommandHandler(ITimeTableRepository timeTableRepository)
    {
        _timeTableRepository  = timeTableRepository;
    }
    public async Task<bool> Handle(UpdateTimeTableCommand request, CancellationToken cancellationToken)
    {
        var timeTable = await _timeTableRepository.GetTimeTableByIdAsync(request.timeTableId);
        if (timeTable == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.timeTableId} not found!");
        }

        timeTable.Audience = request.timeTable.Audience;
        timeTable.StudentGroupId = request.timeTable.StudentGroupId;
        timeTable.SubjectId = request.timeTable.SubjectId;
        timeTable.TeacherId = request.timeTable.TeacherId;

        return await _timeTableRepository.UpdateTimeTableAsync(timeTable, cancellationToken);    
    }
}
