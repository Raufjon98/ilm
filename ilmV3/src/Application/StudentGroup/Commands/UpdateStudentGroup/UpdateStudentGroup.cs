using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Commands.UpdateStudentGroup;
public record UpdateStudentGroupCommand(int studentGroupId, StudentGroupDto studentGroup) : IRequest<StudentGroupVM?>;

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
        if (studentGroup == null)
        {
            return null;
        }
        studentGroup.SubjectId = request.studentGroup.SubjectId;
        studentGroup.Name = request.studentGroup.Name;
        studentGroup.CodeName = request.studentGroup.CodeName;

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
