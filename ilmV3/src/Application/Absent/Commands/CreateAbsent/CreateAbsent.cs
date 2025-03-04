using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Application.Absent.Queries.GetAbsent;

namespace ilmV3.Application.Absent.Commands.CreateAbsent;
public record CreateAbsentCommand(AbsentDto absent) : IRequest<bool>;
public class CreateAbsentCommandHandle : IRequestHandler<CreateAbsentCommand, bool>
{
    private readonly IAbsentRepository _absentRepository;
    private readonly IMapper _mapper;
    public CreateAbsentCommandHandle(IMapper mapper, IAbsentRepository absentRepository)
    {
        _absentRepository = absentRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(CreateAbsentCommand request, CancellationToken cancellationToken)
    {
        var absent = _mapper.Map<AbsentEntity>(request.absent);
       return  await _absentRepository.CreateAbsentAsync(absent, cancellationToken);
    }
}



