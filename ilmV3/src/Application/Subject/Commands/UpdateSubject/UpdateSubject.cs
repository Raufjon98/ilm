using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Subject.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Commands.UpdateSubject;
public record UpdateSubjectCommand(int subjectId, SubjectDto subject) : IRequest<bool>;

public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, bool>
{
    private readonly ISubjectRepository _subjectRepository;
    public UpdateSubjectCommandHandler(ISubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }
    public async Task<bool> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetSubjectByIdAsync(request.subjectId);
        if (subject == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.subjectId} not found");
        }
        subject.Name = request.subject.Name;
        subject.TeacherId = request.subject.TeacherId;
        return await _subjectRepository.UpdateSubjectAsync(subject, cancellationToken);
    }
}
