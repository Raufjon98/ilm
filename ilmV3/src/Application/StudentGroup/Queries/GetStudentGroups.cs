using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.StudentGroup.Queries;

[Authorize(Policy = Policies.CanAdd)]
public record GetStudentGroupsQuery : IRequest<IEnumerable<StudentGroupVM>>;

public class GetStudentGroupsQueryHandler : IRequestHandler<GetStudentGroupsQuery, IEnumerable<StudentGroupVM>>
{
    private readonly IAplicationDbContext _context;
    public GetStudentGroupsQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<StudentGroupVM>> Handle(GetStudentGroupsQuery request, CancellationToken cancellationToken)
    {
        var studentGroups = await _context.StudentGroups.ToListAsync();
        List<StudentGroupVM> result = new List<StudentGroupVM>();
        foreach (var studentGroup in studentGroups)
        {
            StudentGroupVM studentGroupVM = new StudentGroupVM()
            {
                Id = studentGroup.Id,
                Name = studentGroup.Name,
                CodeName = studentGroup.CodeName,
                SubjectId = studentGroup.SubjectId,
            };
            result.Add(studentGroupVM);
        }
        return result;
    }
}
