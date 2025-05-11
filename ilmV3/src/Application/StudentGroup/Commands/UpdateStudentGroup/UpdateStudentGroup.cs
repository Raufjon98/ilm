using ilmV3.Application.Common.Security;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Commands.UpdateStudentGroup;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record UpdateStudentGroupCommand(int studentGroupId, StudentGroupDto StudentGroup) : IRequest<StudentGroupVM?>;

public class UpdateStudentGroupCommandHandler : IRequestHandler<UpdateStudentGroupCommand, StudentGroupVM?>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    public UpdateStudentGroupCommandHandler(IStudentGroupRepository studentGroupRepository)
    {
        _studentGroupRepository = studentGroupRepository;
    }
    public async Task<StudentGroupVM?> Handle(UpdateStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var studentGroup = await _studentGroupRepository.GetStudentGroupByIdAsync(request.studentGroupId);
        ArgumentNullException.ThrowIfNull(studentGroup);

        studentGroup.SubjectId = request.StudentGroup.SubjectId;
        studentGroup.Name = request.StudentGroup.Name;
        studentGroup.CodeName = request.StudentGroup.CodeName;

        var result = await _studentGroupRepository.UpdateStudentGroupAsync(studentGroup, cancellationToken);

        StudentGroupVM studentGroupVM = new StudentGroupVM
        {
            Id = result.Id,
            SubjectId = result.SubjectId,
            Name = result.Name,
            CodeName = result.CodeName,
        };
        return studentGroupVM;
    }
}
