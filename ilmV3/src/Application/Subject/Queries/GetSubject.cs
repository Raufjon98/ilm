using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Subject.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetSubjectQuery(int subjectId) : IRequest<SubjectVM>;

public class GetSubjectQueryHandler : IRequestHandler<GetSubjectQuery, SubjectVM>
{
    private readonly IAplicationDbContext _context;
    public GetSubjectQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SubjectVM> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(x => x.Id == request.subjectId);
        ArgumentNullException.ThrowIfNull(subject);

        SubjectVM subjectVM = new SubjectVM()
        {
            Id = subject.Id,
            Name = subject.Name,
            TeacherId = subject.TeacherId,
        };

        return subjectVM;
    }
}
