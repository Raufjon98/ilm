using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;
public record GetStudentsBySubjectIdQuery(int subjectId) : IRequest<IEnumerable<StudentVM>>;

public class GetStudentsBySubjectIdQueryHandler : IRequestHandler<GetStudentsBySubjectIdQuery, IEnumerable<StudentVM>>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;
    public GetStudentsBySubjectIdQueryHandler(IMapper mapper, ISubjectRepository subjectRepository)
    {
        _mapper = mapper;
        _subjectRepository = subjectRepository;
    }
    public async Task<IEnumerable<StudentVM>> Handle(GetStudentsBySubjectIdQuery request, CancellationToken cancellationToken)
    {
       return _mapper.Map<IEnumerable<StudentVM>>( await _subjectRepository.GetStudentsBySubjectIdAsync(request.subjectId));
        
    }
}
