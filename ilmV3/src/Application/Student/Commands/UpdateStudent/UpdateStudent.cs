using ilmV3.Application.Student.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Commands.UpdateStudent;
public record UpdateStudentCommand(int studentId, StudentDto student) : IRequest<StudentVM?>;
public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentVM?>
{
    private readonly IStudentRepository _studentRepository;
    public UpdateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async Task<StudentVM?> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentRepository.GetStudentByIdAsync(request.studentId);
        if (student == null)
        {
            return null;
        }

        student.Name = request.student.Name;
        await _studentRepository.UpdateStudentAsync(student, cancellationToken);
        StudentVM studentVM = new StudentVM
        {
            Id = student.Id,
            Name = student.Name,
        };
        return studentVM;
    }
}
