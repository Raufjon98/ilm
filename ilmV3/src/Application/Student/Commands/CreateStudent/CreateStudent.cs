using ilmV3.Application.Student.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Student.Commands.CreateStudent;
public record CreateStudentCommand(StudentDto student, string email, string password) : IRequest<StudentVM>;
public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentVM>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IApplicationUserRepository _userRepository;
    public CreateStudentCommandHandler(IStudentRepository studentRepository,
        IApplicationUserRepository userRepository)
    {
        _studentRepository = studentRepository;
        _userRepository = userRepository;
    }
    public async Task<StudentVM> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new StudentEntity()
        {
            Name = request.student.Name,
        };

        StudentEntity studentNew = await _studentRepository.CreateStudentAsync(student, cancellationToken);

        var userNew = await _userRepository.CreateUserAsync(studentNew.Id, studentNew.Name, request.email, request.password);
        if (userNew == null)
        {
            throw new Exception("User does not created!");
        }
        await _userRepository.AddRoleAsync(userNew, "Student");

        StudentVM studentVM = new StudentVM
        {
            Id = studentNew.Id,
            Name = studentNew.Name
        };
        return studentVM;
    }
}
