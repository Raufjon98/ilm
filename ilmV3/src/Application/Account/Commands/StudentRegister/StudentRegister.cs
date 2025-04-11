using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Account.Commands.Register;
public record StudentRegisterCommand(RegisterDto Register) : IRequest<CreatedUserDto?>;
public class StudentRegisterCommandHandler : IRequestHandler<StudentRegisterCommand, CreatedUserDto?>
{
    private readonly IIdentityService _identityService;
    private readonly IStudentRepository _studentRepository;
    private readonly ITokenService _tokenService;
    public StudentRegisterCommandHandler(IIdentityService identityService,
        IStudentRepository studentRepository, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _identityService = identityService;
        _studentRepository = studentRepository;
    }
    public async Task<CreatedUserDto?> Handle(StudentRegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Register);
        StudentEntity student = new StudentEntity
        {
            Name = request.Register.UserName
        };

        StudentEntity createdStudent = await _studentRepository.CreateStudentAsync(student, cancellationToken);
        if (createdStudent == null)
        {
            throw new Exception("Register: Student does not create!");
        }

        var createdUser = await _identityService.CreateUserAsync(createdStudent.Id, request.Register, "Student");
        if (createdUser == null)
        {
            throw new Exception("Register: User does not create!");
        }

        ApplicationUserDto userDto = new ApplicationUserDto
        {
            Id = createdUser.Id,
            UserName = request.Register.UserName,
            Email = request.Register.Email,
            Password = request.Register.Password,
            Role = "Student",
        };

        CreatedUserDto createdUserDto = new CreatedUserDto
        {
            Email = request.Register.Email,
            UserName = request.Register.UserName,
            Token = _tokenService.CreateToken(userDto)
        };
        return createdUserDto;
    }
}
