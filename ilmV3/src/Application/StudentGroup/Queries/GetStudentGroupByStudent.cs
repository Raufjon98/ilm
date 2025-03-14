using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Queries;
public record GetStudentGroupByStudentQuery(int studentId) : IRequest<IEnumerable<StudentGroupVM>>;

public class GetStudentGroupByStudentQueryHandler : IRequestHandler<GetStudentGroupByStudentQuery, IEnumerable<StudentGroupVM>>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly IApplicationDbContext _context;
    public GetStudentGroupByStudentQueryHandler(IStudentGroupRepository studentGroupRepository, IMapper mapper, IApplicationDbContext context)
    {
        _studentGroupRepository = studentGroupRepository;
        _context = context;
    }
    public async Task<IEnumerable<StudentGroupVM>> Handle(GetStudentGroupByStudentQuery request, CancellationToken cancellationToken)
    {
        var studentGroups = await _context.StudentGroups
             .Include(s => s.Students)
             .Where(s => s.Students!.Any(s => s.Id == request.studentId))
             .ToListAsync();
        if (studentGroups == null)
        {
            throw new Exception("The null error!");
        }

        List<StudentGroupVM> result = new List<StudentGroupVM>();

        foreach (var studentGroup in studentGroups)
        {
            StudentGroupVM studentGroupVM = new StudentGroupVM()
            {
                Id = studentGroup.Id,
                Name = studentGroup.Name,
                CodeName = studentGroup.CodeName,
                SubjectId = studentGroup.SubjectId
            };
            result.Add(studentGroupVM);
        }
        return result;
    }
}
