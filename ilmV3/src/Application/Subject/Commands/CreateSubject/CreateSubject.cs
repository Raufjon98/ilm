using ilmV3.Application.Common.Security;
using ilmV3.Application.Subject.Queries;
using ilmV3.Domain.Constants;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Commands.CreateSubject;

[Authorize(Policy = Policies.HOD)]
public record CreateSubjectCommand(SubjectDto Subject) : IRequest<SubjectVM>;

public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, SubjectVM>
{
    private readonly ISubjectRepository _subjectRepository;
    public CreateSubjectCommandHandler(IMapper mapper, ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }
    public async Task<SubjectVM> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        SubjectEntity subject = new SubjectEntity()
        {
            Name = request.Subject.Name,
            TeacherId = request.Subject.TeacherId,
        };
        var result = await _subjectRepository.CreateSubjectAsync(subject, cancellationToken);

        SubjectVM subjectVM = new SubjectVM
        {
            Id = result.Id,
            Name = result.Name,
            TeacherId = result.TeacherId,
        };
        return subjectVM;
    }
}
