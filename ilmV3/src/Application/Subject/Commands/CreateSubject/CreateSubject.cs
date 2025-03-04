using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Subject.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Commands.CreateSubject;
public record CreateSubjectCommand(SubjectDto subject) : IRequest<bool>;

public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, bool>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMapper _mapper;
    public CreateSubjectCommandHandler(IMapper mapper, ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = _mapper.Map<SubjectEntity>(request.subject);
        return await _subjectRepository.CreateSubjectAsync(subject, cancellationToken);
    }
}
