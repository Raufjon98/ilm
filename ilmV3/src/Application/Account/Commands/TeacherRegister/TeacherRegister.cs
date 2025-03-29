using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Account.Commands.Register;
public record TeacherRegisterCommand(RegisterDto register) : IRequest<CreatedUserDto?>;

public class TeacherRegisterCommandHandler : IRequestHandler<TeacherRegisterCommand, CreatedUserDto?>
{
    private readonly IIdentityService _identityService;
    private readonly ITeacherRepository _teacherRepository;
    private readonly ITokenService _tokenService;
    public TeacherRegisterCommandHandler(IIdentityService identityService,
        ITeacherRepository teacherRepository, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _identityService = identityService;
        _teacherRepository = teacherRepository;
    }
    public async Task<CreatedUserDto?> Handle(TeacherRegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.register);
        TeacherEntity teacher = new TeacherEntity()
        {
            Name = request.register.UserName
        };

        TeacherEntity createdTeacher = await _teacherRepository.CreateTeacherAsync(teacher, cancellationToken);
        if (createdTeacher == null)
        {
            throw new Exception("Register: Teacher does not create!");
        }

        var createdUser = await _identityService.CreateUserAsync(createdTeacher.Id, request.register, "Teacher");
        if (createdUser == null)
        {
            throw new Exception("Register: User does not create!");
        }

        ApplicationUserDto userDto = new ApplicationUserDto
        {
            UserName = request.register.UserName,
            Email = request.register.Email,
            Password = request.register.Password,
            Role = "Teacher",
        };

        CreatedUserDto createdUserDto = new CreatedUserDto
        {
            Email = request.register.Email,
            UserName = request.register.UserName,
            Token = _tokenService.CreateToken(userDto)
        };
        return createdUserDto;
    }
}
