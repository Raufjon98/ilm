using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Queries;

[Authorize(Policy = Policies.CanRead)]
public record GetStudentGroupQuery(int studentGroupId) : IRequest<StudentGroupVM>;

public class StudentGroupQueryHandler : IRequestHandler<GetStudentGroupQuery, StudentGroupVM>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    public StudentGroupQueryHandler(IStudentGroupRepository studentGroupRepository)
    {
        _studentGroupRepository = studentGroupRepository;
    }
    public async Task<StudentGroupVM> Handle(GetStudentGroupQuery request, CancellationToken cancellationToken)
    {
        var studentGroup = await _studentGroupRepository.GetStudentGroupByIdAsync(request.studentGroupId);
        if (studentGroup == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.studentGroupId} not found.");
        }

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
