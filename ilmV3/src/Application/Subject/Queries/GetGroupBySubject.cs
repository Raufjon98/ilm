using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;
public record GetGroupBySubjectQuery(int subjectId) : IRequest<StudentGroupVM>;

public class GetGroupBySubjectQueryHandler : IRequestHandler<GetGroupBySubjectQuery, StudentGroupVM>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;
    public GetGroupBySubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _mapper = mapper;
        _subjectRepository = subjectRepository;
    }
    public async Task<StudentGroupVM> Handle(GetGroupBySubjectQuery request, CancellationToken cancellationToken)
    {
        var group = await _subjectRepository.GetGroupBySubjectAsync(request.subjectId);
        return _mapper.Map<StudentGroupVM>(group);
    }
}
