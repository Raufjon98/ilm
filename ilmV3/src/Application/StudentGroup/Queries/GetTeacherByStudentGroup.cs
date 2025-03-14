using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Queries;
public record GetTeacherByStudentGroupQuery(int studentGroupId) : IRequest<TeacherVM>;

public class GetTeacherByStudentGroupQueryHandler : IRequestHandler<GetTeacherByStudentGroupQuery, TeacherVM>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IApplicationDbContext _context;
    public GetTeacherByStudentGroupQueryHandler(IMapper mapper,
        IStudentGroupRepository studentGroupRepository, IApplicationDbContext context)
    {
        _studentGroupRepository = studentGroupRepository;
        _context = context;
    }
    public async Task<TeacherVM> Handle(GetTeacherByStudentGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _context.StudentGroups
             .Include(s => s.Subject)
             .FirstOrDefaultAsync(sg => sg.Id == request.studentGroupId);
        if (group == null)
        {
            throw new Exception("The group does not found!");
        }

        var teacher = await _context.Teachers.
            Include(t => t.Subject)
            .FirstOrDefaultAsync(t => t.Subject != null && t.Subject.Id == group.SubjectId);
        if (teacher == null)
        {
            throw new Exception("Teacher not found!");
        }
        TeacherVM teacherVM = new TeacherVM()
        {
            Id = teacher.Id,
            Name = teacher.Name,
        };

        return teacherVM;
    }
}
