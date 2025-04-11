using ilmV3.Application.Common.Security;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Constants;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Commands.CreateStudentGroup;

[Authorize(Policy = Policies.CanAdd)]
public record CreateStudentGroupCommand(StudentGroupDto StudentGroup) : IRequest<StudentGroupVM>;

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
            CodeName = request.StudentGroup.CodeName,
            Name = request.StudentGroup.Name,
            SubjectId = request.StudentGroup.SubjectId,
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
