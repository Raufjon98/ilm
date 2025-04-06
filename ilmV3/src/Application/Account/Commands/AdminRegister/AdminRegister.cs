using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Account.Commands.Register;
public record AdminRegisterCommand(RegisterDto register) : IRequest<CreatedUserDto?>;

public class AdminRegisterCommandHandler : IRequestHandler<AdminRegisterCommand, CreatedUserDto?>
{
    private readonly IIdentityService _identityService;
    private readonly IStudentRepository _studentRepository;
    private readonly ITokenService _tokenService;
    public AdminRegisterCommandHandler(IIdentityService identityService,
        IStudentRepository studentRepository, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _identityService = identityService;
        _studentRepository = studentRepository;
    }
    public async Task<CreatedUserDto?> Handle(AdminRegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.register);
        var createdUser = await _identityService.CreateUserAsync(12, request.register, "Administrator");
        if (createdUser == null)
        {
            throw new Exception("Register: User does not create!");
        }

        ApplicationUserDto userDto = new ApplicationUserDto
        {
            Id = createdUser.Id,
            UserName = request.register.UserName,
            Email = request.register.Email,
            Password = request.register.Password,
            Role = "Admin",
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
