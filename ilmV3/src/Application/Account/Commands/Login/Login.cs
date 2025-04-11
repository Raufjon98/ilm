using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Account.Commands.Login;
public record LoginCommand(LoginDto Login) : IRequest<CreatedUserDto?>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, CreatedUserDto?>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;
    public LoginCommandHandler(IIdentityService identityService, ITokenService tokenService)
    {
        _identityService = identityService;
        _tokenService = tokenService;
    }

    public async Task<CreatedUserDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Login);

        var user = await _identityService.GetUserByUsernameAsync(request.Login.Email);
        ArgumentNullException.ThrowIfNull(user);

        var roles = await _identityService.GetUserRolesAsync(user);
        ArgumentNullException.ThrowIfNull(roles);

        var result = await _identityService.CheckPasswordAsync(user, request.Login.Password);

        if (!result)
        {
            throw new UnauthorizedAccessException();
        }

        IApplicationUser? userFromDb = await _identityService.GetUserByUsernameAsync(request.Login.Email);
        ArgumentNullException.ThrowIfNull(userFromDb);
        ApplicationUserDto applicationUserDto = new ApplicationUserDto
        {
            Email = request.Login.Email,
            UserName = user?.UserName ?? "",
            Id = userFromDb.Id,
            Role = string.Join(",", roles.ToArray())
        };

        CreatedUserDto createdUserDto = new CreatedUserDto
        {
            UserName = user?.UserName ?? "",
            Email = request.Login.Email,
            Token = _tokenService.CreateToken(applicationUserDto)

        };
        return createdUserDto;
    }
}
