using ilmV3.Application.Account.Commands.Register;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Teacher.Commands.CreateTeacher;
public record CreateTeacherCommand(RegisterDto register) : IRequest<TeacherVM?>;

public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, TeacherVM?>
{
    private readonly ITeacherRepository _teacherRepository;
    private readonly IIdentityService _identityService;
    public CreateTeacherCommandHandler(ITeacherRepository teacherRepository, IIdentityService identityService)
    {
        _identityService = identityService;
        _teacherRepository = teacherRepository;
    }
    public async Task<TeacherVM?> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
    {
        TeacherEntity teacher = new TeacherEntity
        {
            Name = request.register.UserName,
        };
        var teacherNew = await _teacherRepository.CreateTeacherAsync(teacher, cancellationToken);

        if (teacherNew == null)
            return null;

        var result = await _identityService.CreateUserAsync(teacherNew.Id, request.register);

        if (result == null)
            return null;


        TeacherVM teacherVM = new TeacherVM
        {
            Id = teacherNew.Id,
            Name = teacherNew.Name

        };
        return teacherVM;
    }
}
