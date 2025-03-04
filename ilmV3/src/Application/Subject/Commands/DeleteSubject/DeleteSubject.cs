using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Commands.DeleteSubject;
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
        if (subject == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.SubjectId} not dound");
        }
        return await _subjectRepository.DeleteSubjectAsync(subject, cancellationToken);
    }
}
