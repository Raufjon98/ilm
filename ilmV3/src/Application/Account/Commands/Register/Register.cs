using System.Diagnostics.Tracing;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Account.Commands.Register;
public record RegisterCommand(RegisterDto register) : IRequest<CreatedUserDto?>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, CreatedUserDto?>
{
    private readonly IIdentityService _identityService;
    private readonly IStudentRepository _studentRepository;
    private readonly ITokenService _tokenService;
    public RegisterCommandHandler(IIdentityService identityService,
        IStudentRepository studentRepository, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _identityService = identityService;
        _studentRepository = studentRepository;
    }
    public async Task<CreatedUserDto?> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        StudentEntity student = new StudentEntity
        {
            Name = request.register.UserName
        };

        StudentEntity createdStudent = await _studentRepository.CreateStudentAsync(student, cancellationToken);
        if (createdStudent == null)
        {
            throw new Exception("Register: Student does not create!");
        }

        var createdUser = await _identityService.CreateUserAsync(createdStudent.Id, request.register);
        if (createdUser == null)
        {
            throw new Exception("Register: User does not create!");
        }

        ApplicationUserDto userDto = new ApplicationUserDto
        {
            UserName = request.register.UserName,
            Email = request.register.Email,
            Password = request.register.Password,
            Role = request.register.Role,
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
