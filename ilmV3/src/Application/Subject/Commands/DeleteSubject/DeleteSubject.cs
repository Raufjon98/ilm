using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Commands.DeleteSubject;

[Authorize(Policy = Policies.HOD)]
public record DeleteSubjectCommand(int SubjectId) : IRequest<bool>;

public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, bool>
{
    private readonly ISubjectRepository _subjectRepository;
    public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }
    public async Task<bool> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetSubjectByIdAsync(request.SubjectId);
        ArgumentNullException.ThrowIfNull(subject);
        return await _subjectRepository.DeleteSubjectAsync(subject, cancellationToken);
    }
}
