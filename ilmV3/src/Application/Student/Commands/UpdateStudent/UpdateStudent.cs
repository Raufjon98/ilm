using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Commands.UpdateStudent;
public record UpdateStudentCommand(string studentId, StudentDto student) : IRequest<StudentVM?>;
public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentVM?>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IIdentityService _identityService;
    public UpdateStudentCommandHandler(IStudentRepository studentRepository, IIdentityService identityService)
    {
        _identityService = identityService;
        _studentRepository = studentRepository;
    }
    public async Task<StudentVM?> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserByIdAsync(request.studentId);
        ArgumentNullException.ThrowIfNull(user);
        
        user.UserName = request.student.Name;

        await _identityService.UpdateUserAsync(user);

        var student = await _studentRepository.GetStudentByIdAsync(user.ExternalUserId);
        ArgumentNullException.ThrowIfNull(student);

        student.Name = request.student.Name;

        var studentResult = await _studentRepository.UpdateStudentAsync(student, cancellationToken);

        if (studentResult == null)
        {
            return null;
        }

        StudentVM studentVM = new StudentVM
        {
            Id = student.Id,
            Name = student.Name,
        };
        return studentVM;
    }
}
