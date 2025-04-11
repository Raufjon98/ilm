using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetSubjectsQuery : IRequest<IEnumerable<SubjectVM>>;

public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, IEnumerable<SubjectVM>>
{
    private readonly ISubjectRepository _subjectRepository;
    public GetSubjectsQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
    {
        _subjectRepository = subjectRepository;
    }
    public async Task<IEnumerable<SubjectVM>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        var subjects = await _subjectRepository.GetSubjectsAsync();

        List<SubjectVM> result = new List<SubjectVM>();
        foreach (var subject in subjects)
        {
            SubjectVM subjectVM = new SubjectVM()
            {
                Id = subject.Id,
                Name = subject.Name,
                TeacherId = subject.TeacherId,
            };
        }
        return result;
    }
}

