using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Account.Commands.Register;
public record AdminRegisterCommand(RegisterDto Register) : IRequest<CreatedUserDto?>;

public class AdminRegisterCommandHandler : IRequestHandler<AdminRegisterCommand, CreatedUserDto?>
{
    private readonly IIdentityService _identityService;
    private readonly IAdminRepository _adminRepository;
    private readonly ITokenService _tokenService;
    private const string role = "Administrator";
    public AdminRegisterCommandHandler(IIdentityService identityService,
       IAdminRepository adminRepository, ITokenService tokenService)
    {
        _adminRepository = adminRepository;
        _tokenService = tokenService;
        _identityService = identityService;
    }
    public async Task<CreatedUserDto?> Handle(AdminRegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Register);

        AdminEntity admin = new AdminEntity
        {
            Name = request.Register.UserName,
        };

        var createdAdmin = await _adminRepository.CreateAdminAsync(admin, cancellationToken);
        ArgumentNullException.ThrowIfNull(createdAdmin);

        var createdUser = await _identityService.CreateUserAsync(createdAdmin.Id, request.Register, role);
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
            Role = "Admin",
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
