
namespace ilmV3.Application.Account.Login;
public record LoginCommand(LoginDto login) : IRequest<bool>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
{
    public Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
