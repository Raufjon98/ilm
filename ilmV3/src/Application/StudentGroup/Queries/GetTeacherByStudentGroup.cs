using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.StudentGroup.Queries;

[Authorize(Policy = Policies.CanAdd)]
public record GetTeacherByStudentGroupQuery(int studentGroupId) : IRequest<TeacherVM>;

public class GetTeacherByStudentGroupQueryHandler : IRequestHandler<GetTeacherByStudentGroupQuery, TeacherVM>
{
    private readonly IAplicationDbContext _context;
    public GetTeacherByStudentGroupQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<TeacherVM> Handle(GetTeacherByStudentGroupQuery request, CancellationToken cancellationToken)
    {
        var studentGroup = await _context.StudentGroups
             .Include(s => s.Subject)
             .FirstOrDefaultAsync(sg => sg.Id == request.studentGroupId);
        ArgumentNullException.ThrowIfNull(studentGroup);

        var teacher = await _context.Teachers.
            Include(t => t.Subject)
            .FirstOrDefaultAsync(t => t.Subject != null && t.Subject.Id == studentGroup.SubjectId);
        ArgumentNullException.ThrowIfNull(teacher);
        TeacherVM teacherVM = new TeacherVM()
        {
            Id = teacher.Id,
            Name = teacher.Name,
        };

        return teacherVM;
    }
}
