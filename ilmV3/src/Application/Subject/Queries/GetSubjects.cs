using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Subject.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetSubjectsQuery : IRequest<IEnumerable<SubjectVM>>;

public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, IEnumerable<SubjectVM>>
{
    private readonly IApplicationDbContext _context;
    public GetSubjectsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<SubjectVM>> Handle(GetSubjectsQuery request, CancellationToken cancellationToken)
    {
        var subjects = await _context.Subjects.ToListAsync();

        List<SubjectVM> result = new List<SubjectVM>();
        foreach (var subject in subjects)
        {
            SubjectVM subjectVM = new SubjectVM()
            {
                Id = subject.Id,
                Name = subject.Name,
                TeacherId = subject.TeacherId,
            };
            result.Add(subjectVM);
        }
        return result;
    }
}

