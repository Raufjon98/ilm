using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Absent.Queries.GetAbsent;
public record GetAbsentByIdQuery (int absentId) : IRequest<AbsentVM>;

public class GetAbsentByIdQueryHandler : IRequestHandler<GetAbsentByIdQuery, AbsentVM>
{
    private readonly IAbsentRepository _absentRepository;
    private readonly IMapper _mapper;
    public GetAbsentByIdQueryHandler(IAbsentRepository absentRepository, IMapper mapper)
    {
        _mapper = mapper;
        _absentRepository = absentRepository;
    }
    public async Task<AbsentVM> Handle(GetAbsentByIdQuery request, CancellationToken cancellationToken)
    {
        var absent =  await _absentRepository.GetAbsentByIdAsync(request.absentId);
        if (absent == null)
        {
            throw new KeyNotFoundException($"Absent record with ID {request.absentId} not found.");
        }
        return _mapper.Map<AbsentVM>(absent);
    }
}
