using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Teacher.Queries;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record GetTeacherQuery(int teacherId) : IRequest<TeacherVM>;

public class GetTeacherQueryHandler : IRequestHandler<GetTeacherQuery, TeacherVM>
{
    private readonly IAplicationDbContext _context;
    public GetTeacherQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<TeacherVM> Handle(GetTeacherQuery request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == request.teacherId);
        ArgumentNullException.ThrowIfNull(teacher);

        TeacherVM teacherVM = new TeacherVM()
        {
            Id = teacher.Id,
            Name = teacher.Name,
        };
        return teacherVM;
    }
}
