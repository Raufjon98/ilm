using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.Constants;
using ilmV3.Domain.interfaces;

namespace ilmV3.Application.Account.Commands.Login;
public record LoginCommand(LoginDto login) : IRequest<CreatedUserDto?>;

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
        ArgumentNullException.ThrowIfNull(request.login);

        var user = await _identityService.GetUserByUsernameAsync(request.login.Email);
        ArgumentNullException.ThrowIfNull(user);

        var result = await _identityService.CheckPasswordAsync(user, request.login.Password);

        if (!result)
        {
            throw new UnauthorizedAccessException();
        }

        IApplicationUser? userFromDb = await _identityService.GetUserByUsernameAsync(request.login.Email);
        ArgumentNullException.ThrowIfNull(userFromDb);
        ApplicationUserDto applicationUserDto = new ApplicationUserDto
        {
            Email = request.login.Email,
            Id = userFromDb.Id,
            Role = "RoleIsHardCoded!"

        };

        CreatedUserDto createdUserDto = new CreatedUserDto
        {
            UserName = user?.UserName ?? "UnknownUser",
            Email = request.login.Email,
            Token = _tokenService.CreateToken(applicationUserDto)

        };
        return createdUserDto;
    }
}
