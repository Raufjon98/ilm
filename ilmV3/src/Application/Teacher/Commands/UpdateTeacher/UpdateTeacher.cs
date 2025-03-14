using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Commands.UpdateTeacher;
public record UpdateTeacherCommand(int teacherId, TeacherDto teacher) : IRequest<TeacherVM?>;

public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, TeacherVM?>
{
    private readonly ITeacherRepository _teacherRepository;
    public UpdateTeacherCommandHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }
    public async Task<TeacherVM?> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _teacherRepository.GetTeacherByIdAsync(request.teacherId);
        if (teacher == null)
        {
            return null;
        }
        teacher.Name = request.teacher.Name;
        var result = await _teacherRepository.UpdateTeacherAsync(teacher, cancellationToken);
        TeacherVM teacherVM = new TeacherVM
        {
            Id = result.Id,
            Name = result.Name,
        };
        return teacherVM;
    }
}
