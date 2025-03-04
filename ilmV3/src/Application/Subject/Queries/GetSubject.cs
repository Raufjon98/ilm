using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;
public record GetSubjectQuery(int subjectId) : IRequest<SubjectVM>;

public class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, SubjectVM>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;
    public GetSubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }
    public async Task<SubjectVM> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
    {
        var result = await _subjectRepository.GetSubjectByIdAsync(request.subjectId);
        if (result == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.subjectId} not found");
        }
        return _mapper.Map<SubjectVM>(result);
    }
}
