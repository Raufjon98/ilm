using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.Constants;

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
        if (request.login == null)
        {
            throw new Exception("Invalid Login data!");
        }

        var user = await _identityService.GetUserByUsernameAsync(request.login.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid username or password!");
        }


        var result = await _identityService.CheckPasswordAsync(user, request.login.Password);

        ApplicationUserDto applicationUserDto = new ApplicationUserDto
        {
            Email = request.login.Email,
            Password = request.login.Password
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
