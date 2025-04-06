using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Commands.UpdateTeacher;
public record UpdateTeacherCommand(string teacherId, TeacherDto teacher) : IRequest<TeacherVM?>;

public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, TeacherVM?>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IIdentityService _identityService;
    public UpdateTeacherCommandHandler(ITeacherRepository teacherRepository, IIdentityService identityService)
    {
        _identityService = identityService;
        _teacherRepository = teacherRepository;
    }
    public async Task<TeacherVM?> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserByIdAsync(request.teacherId);
        ArgumentNullException.ThrowIfNull(user);

        user.UserName = request.teacher.Name;

        await _identityService.UpdateUserAsync(user);

        var teacher = await _teacherRepository.GetTeacherByIdAsync(user.ExternalUserId);
        ArgumentNullException.ThrowIfNull(teacher);

        teacher.Name = request.teacher.Name;

        var result = await _teacherRepository.UpdateTeacherAsync(teacher, cancellationToken);
        if (result == null)
            return null;

        TeacherVM teacherVM = new TeacherVM
        {
            Id = result.Id,
            Name = result.Name,
        };
        return teacherVM;
    }
}
