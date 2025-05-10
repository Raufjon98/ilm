using ilmV3.Application.Common.Security;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Commands.DeleteStudent;

[Authorize(Policy = Policies.CanUpdateAndDelete)]
public record DeleteStudentCommand(int studentId) : IRequest<bool>;
public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
{
    private readonly IStudentRepository _studentRepository;
    public DeleteStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.studentId);
        ArgumentNullException.ThrowIfNull(student);
        return await _studentRepository.DeleteStudentAsync(student, cancellationToken);
    }
}
