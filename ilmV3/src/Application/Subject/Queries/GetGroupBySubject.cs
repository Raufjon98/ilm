using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Security;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Constants;

namespace ilmV3.Application.Subject.Queries;

[Authorize(Policy = Policies.CanAdd)]
public record GetGroupBySubjectQuery(int subjectId) : IRequest<StudentGroupVM>;

public class GetGroupBySubjectQueryHandler : IRequestHandler<GetGroupBySubjectQuery, StudentGroupVM>
{
    private readonly IAplicationDbContext _context;
    public GetGroupBySubjectQueryHandler(
        IAplicationDbContext context)
    {
        _context = context;
    }
    public async Task<StudentGroupVM> Handle(GetGroupBySubjectQuery request, CancellationToken cancellationToken)
    {
        var studentGroup = await _context.StudentGroups.FirstOrDefaultAsync(s => s.SubjectId == request.subjectId);
        ArgumentNullException.ThrowIfNull(studentGroup);

        StudentGroupVM studentGroupVM = new StudentGroupVM()
        {
            Id = studentGroup.Id,
            Name = studentGroup.Name,
            CodeName = studentGroup.CodeName,
            SubjectId = studentGroup.SubjectId,
        };
        return studentGroupVM;
    }
}
