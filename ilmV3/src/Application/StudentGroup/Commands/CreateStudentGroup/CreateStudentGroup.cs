using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Commands.CreateStudentGroup;
public record CreateStudentGroupCommand(StudentGroupDto studentGroup) : IRequest<StudentGroupVM>;

public class CreateStudentGroupCommandHandler : IRequestHandler<CreateStudentGroupCommand, StudentGroupVM>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    public CreateStudentGroupCommandHandler(IMapper mapper, IStudentGroupRepository studentGroupRepository)
    {
        _studentGroupRepository = studentGroupRepository;
    }
    public async Task<StudentGroupVM> Handle(CreateStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var studentGroup = new StudentGroupEntity()
        {
            CodeName = request.studentGroup.CodeName,
            Name = request.studentGroup.Name,
            SubjectId = request.studentGroup.SubjectId,
        };
        var result = await _studentGroupRepository.CreateStudentGroupAsync(studentGroup, cancellationToken);
        StudentGroupVM studentGroupVM = new StudentGroupVM
        {
            Id = result.Id,
            CodeName = result.CodeName,
            Name = result.Name,
            SubjectId = result.SubjectId,
        };
        return studentGroupVM;
    }
}
