using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Subject.Queries;

[Authorize(Policy = Policies.CanAdd)]
public record GetSubjectByTeacherQuery(int teacherId) : IRequest<SubjectVM>;

public class GetSubjectByTeacherQueryHandler : IRequestHandler<GetSubjectByTeacherQuery, SubjectVM>
{
    private readonly IApplicationDbContext _context;
    public GetSubjectByTeacherQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<SubjectVM> Handle(GetSubjectByTeacherQuery request, CancellationToken cancellationToken)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.TeacherId == request.teacherId);
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
