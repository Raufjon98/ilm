using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Subject.Queries;
public record GetStudentsBySubjectIdQuery(int subjectId) : IRequest<IEnumerable<StudentVM>>;

public class GetStudentsBySubjectIdQueryHandler : IRequestHandler<GetStudentsBySubjectIdQuery, IEnumerable<StudentVM>>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IApplicationDbContext _context;
    public GetStudentsBySubjectIdQueryHandler(IMapper mapper,
        ISubjectRepository subjectRepository, IApplicationDbContext context)
    {
        _subjectRepository = subjectRepository;
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
