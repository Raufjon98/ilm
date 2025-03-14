using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Queries;
public record GetExcellentStudentsQuery() : IRequest<IEnumerable<StudentWithGradeVM>>;

public class GetExcellentStudentsQueryHandler : IRequestHandler<GetExcellentStudentsQuery, IEnumerable<StudentWithGradeVM>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IApplicationDbContext _context;
    public GetExcellentStudentsQueryHandler(IStudentRepository studentRepository, IMapper mapper, IApplicationDbContext context)
    {
        _studentRepository = studentRepository;
        _context = context;
    }
    public async Task<IEnumerable<StudentWithGradeVM>> Handle(GetExcellentStudentsQuery request, CancellationToken cancellationToken)
    {
        var excellents = await _context.Students
              .SelectMany(s => s.Grades!.Where(g => g.Grade >= 8),
                (s, g) => new { s.Id, s.Name, g.Grade })
                .ToListAsync();
        List<StudentWithGradeVM> result = new List<StudentWithGradeVM>();
        foreach (var excellent in excellents)
        {
            var studentWithGrade = new StudentWithGradeVM()
            {
                Id = excellent.Id,
                Name = excellent.Name,
                Grade = excellent.Grade
            };
            result.Add(studentWithGrade);
        }
        return result;
    }
}
