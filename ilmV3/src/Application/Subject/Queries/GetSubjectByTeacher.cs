using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;
public record GetSubjectByTeacherQuery(int teacherId) : IRequest<SubjectVM>;

public class GetSubjectByTeacherQueryHandler : IRequestHandler<GetSubjectByTeacherQuery, SubjectVM>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;
    public GetSubjectByTeacherQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }
    public async Task<SubjectVM> Handle(GetSubjectByTeacherQuery request, CancellationToken cancellationToken)
    {
        var result = await _subjectRepository.GetSubjectByTeacherAsync(request.teacherId);
        return _mapper.Map<SubjectVM>(result);
    }
}
