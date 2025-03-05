using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;
public record GetTeacherBySubjectQuery(int subjectId) : IRequest<TeacherVM>;

public class GetTecherBySubjectQueryHandler : IRequestHandler<GetTeacherBySubjectQuery, TeacherVM>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;
    public GetTecherBySubjectQueryHandler(IMapper mapper, ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }
    public async Task<TeacherVM> Handle(GetTeacherBySubjectQuery request, CancellationToken cancellationToken)
    {
        var result = await _subjectRepository.GetTeacherBySubject(request.subjectId);
        if (result == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.subjectId} not found");
        }
        return _mapper.Map<TeacherVM>(result);
    }
}
