using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Queries;
public record GetGradesQuery : IRequest<IEnumerable<GradeVM>>;

public class GetGradesQueryHandler : IRequestHandler<GetGradesQuery, IEnumerable<GradeVM>>
{
    private readonly IGradeRepository _gradeRepository;
    private readonly IMapper _mapper;
    public GetGradesQueryHandler(IGradeRepository gradeRepository, IMapper mapper)
    {
        _gradeRepository = gradeRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<GradeVM>> Handle(GetGradesQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<GradeVM>>( await _gradeRepository.GetGradesAsync());
    }
}
