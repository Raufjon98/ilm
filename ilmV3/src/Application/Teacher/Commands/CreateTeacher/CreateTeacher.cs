using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Commands.CreateTeacher;
public record CreateTeacherCommand(TeacherDto teacher, string email, string password) : IRequest<TeacherVM>;

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, TeacherVM>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IApplicationUserRepository _userRepository;
    public CreateTeacherCommandHandler(ITeacherRepository teacherRepository, IApplicationUserRepository userRepository)
    {
        _teacherRepository = teacherRepository;
        _userRepository = userRepository;
    }
    public async Task<TeacherVM> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        TeacherEntity teacher = new TeacherEntity
        {
            Name = request.teacher.Name,
        };
        var teacherNew = await _teacherRepository.CreateTeacherAsync(teacher, cancellationToken);

        if (teacherNew == null)
        {
            throw new Exception("Teacher does not created!");
        }
        var userNew = await _userRepository.CreateUserAsync(teacherNew.Id, teacherNew.Name, request.email, request.password);
        if (userNew == null)
        {
            throw new Exception("User does not created!");
        }
        await _userRepository.AddRoleAsync(userNew, "Teacher");

        TeacherVM teacherVM = new TeacherVM
        {
            Id = teacherNew.Id,
            Name = teacherNew.Name

        };
        return teacherVM;
    }
}
