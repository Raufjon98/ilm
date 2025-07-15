using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Teacher.Queries;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record GetTeachersQuery : IRequest<IEnumerable<TeacherVM>>;

public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, IEnumerable<TeacherVM>>
{
    private readonly IAplicationDbContext _context;
    public GetTeachersQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<TeacherVM>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
    {
        var teachers = await _context.Teachers.ToListAsync();
        List<TeacherVM> result = new List<TeacherVM>();

        foreach (var teacher in teachers)
        {
            TeacherVM teacherVM = new TeacherVM()
            {
                Id = teacher.Id,
                Name = teacher.Name,
            };
            result.Add(teacherVM);
        }
        return result;
    }
}
