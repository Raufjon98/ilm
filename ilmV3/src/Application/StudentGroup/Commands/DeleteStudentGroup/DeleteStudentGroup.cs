using ilmV3.Domain.interfaces;

namespace ilmV3.Application.StudentGroup.Commands.DeleteStudentGroup;
public record DeleteStudentGroupCommand(int studentGroupId) : IRequest<bool>;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentGroupCommand, bool>
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    public DeleteStudentCommandHandler(IStudentGroupRepository studentGroupRepository)
    {
        _studentGroupRepository = studentGroupRepository;
    }

    public async Task<bool> Handle(DeleteStudentGroupCommand request, CancellationToken cancellationToken)
    {
        var studentGroup = await _studentGroupRepository.GetStudentGroupByIdAsync(request.studentGroupId);
        if (studentGroup == null)
        {
            throw new KeyNotFoundException($"Record with ID {request.studentGroupId} not found.");
        }
        return await _studentGroupRepository.DeleteStudentGroupAsync(studentGroup, cancellationToken);
    }
}
