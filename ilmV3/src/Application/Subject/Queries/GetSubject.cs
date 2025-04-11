using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetSubjectQuery(int subjectId) : IRequest<SubjectVM>;

public class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, SubjectVM>
{
    private readonly ISubjectRepository _subjectRepository;
    public GetSubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
    }
    public async Task<SubjectVM> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetSubjectByIdAsync(request.subjectId);
        if (subject == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.subjectId} not found");
        }

        SubjectVM subjectVM = new SubjectVM()
        {
            Id = subject.Id,
            Name = subject.Name,
            TeacherId = subject.TeacherId,
        };

        return subjectVM;
    }
}
