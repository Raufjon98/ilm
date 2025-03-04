using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.TimeTable.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.TimeTable.Commands.CreateTimeTable;
public record CreateTimeTableCommand(TimeTableDto timeTable) : IRequest<bool>;

public class CreateTimeTableCommandHandler : IRequestHandler<CreateTimeTableCommand, bool>
{
    private readonly ITimeTableRepository _timeTableRepository;
    private readonly IMapper _mapper;
    public CreateTimeTableCommandHandler(ITimeTableRepository timeTableRepository, IMapper mapper)
    {
         _timeTableRepository = timeTableRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(CreateTimeTableCommand request, CancellationToken cancellationToken)
    {
        var timeTable = _mapper.Map<TimeTableEntity>(request.timeTable);
        return await _timeTableRepository.CreateTimeTableAsync(timeTable, cancellationToken);
    }
}
