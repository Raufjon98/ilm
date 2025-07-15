using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Grade.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetGradesQuery : IRequest<IEnumerable<GradeVM>>;

public class GetGradesQueryHandler : IRequestHandler<GetGradesQuery, IEnumerable<GradeVM>>
{
    private readonly IAplicationDbContext _context;
    public GetGradesQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<GradeVM>> Handle(GetGradesQuery request, CancellationToken cancellationToken)
    {
        var grades = await _context.Grades.ToListAsync();
        List<GradeVM> result = new List<GradeVM>();
        foreach (var grade in grades)
        {
            var gradeVM = new GradeVM()
            {
                Id = grade.Id,
                StudentId = grade.StudentId,
                TeacherId = grade.TeacherId,
                SubjectId = grade.SubjectId,
                ClassDay = grade.ClassDay,
                Date = grade.Date,
                Grade = grade.Grade
            };
            result.Add(gradeVM);
        }
        return result;
    }
}
