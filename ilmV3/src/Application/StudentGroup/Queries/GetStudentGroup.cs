using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetStudentGroupQuery(int studentGroupId) : IRequest<StudentGroupVM>;

public class StudentGroupQueryHandler : IRequestHandler<GetStudentGroupQuery, StudentGroupVM>
{
    private readonly IAplicationDbContext _context;
    public StudentGroupQueryHandler(IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<StudentGroupVM> Handle(GetStudentGroupQuery request, CancellationToken cancellationToken)
    {
        var studentGroup = await _context.StudentGroups.FirstOrDefaultAsync(x => x.Id == request.studentGroupId);
        ArgumentNullException.ThrowIfNull(studentGroup);

        StudentGroupVM StudentGroupVM = new StudentGroupVM()
        {
            Id = studentGroup.Id,
            Name = studentGroup.Name,
            CodeName = studentGroup.CodeName,
            SubjectId = studentGroup.SubjectId,
        };
        return StudentGroupVM;

    }
}
