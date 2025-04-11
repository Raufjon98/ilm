using ilmV3.Application.Common.Security;
using ilmV3.Application.Subject.Queries;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Commands.UpdateSubject;

[Authorize(Policy = Policies.HOD)]
public record UpdateSubjectCommand(int subjectId, SubjectDto Subject) : IRequest<SubjectVM?>;

public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, SubjectVM?>
{
    private readonly ISubjectRepository _subjectRepository;
    public UpdateSubjectCommandHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }
    public async Task<SubjectVM?> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetSubjectByIdAsync(request.subjectId);
        if (subject == null)
        {
            return null;
        }
        subject.Name = request.Subject.Name;
        subject.TeacherId = request.Subject.TeacherId;

        var result = await _subjectRepository.UpdateSubjectAsync(subject, cancellationToken);

        SubjectVM subjectVM = new SubjectVM
        {
            Id = result.Id,
            Name = result.Name,
            TeacherId = result.TeacherId,
        };
        return subjectVM;
    }
}
