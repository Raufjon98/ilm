using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetStudentGroupByStudentQuery(int studentId) : IRequest<IEnumerable<StudentGroupVM>>;

public class GetStudentGroupByStudentQueryHandler : IRequestHandler<GetStudentGroupByStudentQuery, IEnumerable<StudentGroupVM>>
{
    private readonly IApplicationDbContext _context;
    public GetStudentGroupByStudentQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<StudentGroupVM>> Handle(GetStudentGroupByStudentQuery request, CancellationToken cancellationToken)
    {
        var studentGroups = await _context.StudentGroups
             .Include(s => s.Students)
             .Where(s => s.Students!.Any(s => s.Id == request.studentId))
             .ToListAsync();
        ArgumentNullException.ThrowIfNull(studentGroups);

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
