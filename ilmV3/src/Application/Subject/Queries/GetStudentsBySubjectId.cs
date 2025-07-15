using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Subject.Queries;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record GetStudentsBySubjectIdQuery(int subjectId) : IRequest<IEnumerable<StudentVM>>;

public class GetStudentsBySubjectIdQueryHandler : IRequestHandler<GetStudentsBySubjectIdQuery, IEnumerable<StudentVM>>
{
    private readonly IAplicationDbContext _context;
    public GetStudentsBySubjectIdQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<StudentVM>> Handle(GetStudentsBySubjectIdQuery request, CancellationToken cancellationToken)
    {
        var studentList = await _context.Students
        .Include(s => s.Groups)
              .Where(s => s.Groups!.Any(sm => sm.SubjectId == request.subjectId))
              .ToListAsync();
        List<StudentVM> result = new List<StudentVM>();

        foreach (var student in studentList)
        {
            StudentVM studentVM = new StudentVM()
            {
                Id = student.Id,
                Name = student.Name,
            };
            result.Add(studentVM);
        }
        return result;
    }
}
