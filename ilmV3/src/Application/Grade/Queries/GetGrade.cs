using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Grade.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetGradeQuery(int gradeId) : IRequest<GradeVM>;

public class GetGradeQueryHandler : IRequestHandler<GetGradeQuery, GradeVM>
{
    private readonly IAplicationDbContext _context;
    public GetGradeQueryHandler( IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<GradeVM> Handle(GetGradeQuery request, CancellationToken cancellationToken)
    {
        var grade = await _context.Grades.FirstOrDefaultAsync(x => x.Id == request.gradeId);
        ArgumentNullException.ThrowIfNull(grade);
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
        return gradeVM;
    }
}
