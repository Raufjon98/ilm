using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Queries;
public record GetExcellentStudentsQuery() : IRequest<IEnumerable<StudentWithGradeVM>>;

public class GetExcellentStudentsQueryHandler : IRequestHandler<GetExcellentStudentsQuery, IEnumerable<StudentWithGradeVM>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    public GetExcellentStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _mapper = mapper;
        _studentRepository = studentRepository;
    }
    public async Task<IEnumerable<StudentWithGradeVM>> Handle(GetExcellentStudentsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<StudentWithGradeVM>>(await _studentRepository.GetExcellentStudentsAsync());
    }
}
