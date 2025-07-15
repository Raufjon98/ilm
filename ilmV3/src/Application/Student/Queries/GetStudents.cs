using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Student.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetStudentsQuery : IRequest<IEnumerable<StudentVM>>;

public class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, IEnumerable<StudentVM>>
{
    private readonly IAplicationDbContext _context;
    public GetStudentsQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<StudentVM>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _context.Students.ToListAsync();
        List<StudentVM> result = new List<StudentVM>();
        foreach (var student in students)
        {
            StudentVM studentVM = new StudentVM()
            {
                Id = student.Id,
                Name = student.Name
            };
            result.Add(studentVM);
        }
        return result;
    }
}
