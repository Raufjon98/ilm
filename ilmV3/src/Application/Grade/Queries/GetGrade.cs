using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Queries;
public record GetGradeQuery(int gradeId) : IRequest<GradeVM>;

public class GetGradeQueryHandler : IRequestHandler<GetGradeQuery, GradeVM>
{
    private readonly IMapper _mapper;
    private readonly IGradeRepository _gradeRepository;
    public GetGradeQueryHandler(IGradeRepository gradeRepository, IMapper mapper)
    {
        _gradeRepository = gradeRepository;
        _mapper = mapper;
    }
    public async Task<GradeVM> Handle(GetGradeQuery request, CancellationToken cancellationToken)
    {
       var result = await _gradeRepository.GetGradeByIdAsync(request.gradeId);
        if (result == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.gradeId} not found.");
        }
        return _mapper.Map<GradeVM>(result);
    }
}
