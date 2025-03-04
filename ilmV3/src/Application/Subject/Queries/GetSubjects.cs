using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;

public record GetSubjectsQuery : IRequest<IEnumerable<SubjectVM>>;

public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, IEnumerable<SubjectVM>>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;
    public GetSubjectsQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<SubjectVM>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<IEnumerable<SubjectVM>>(await _subjectRepository.GetSubjectsAsync());
    }
}

