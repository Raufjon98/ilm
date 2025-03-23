
namespace ilmV3.Application.Account.Queries;
public record UserQuery : IRequest<string>;

public class UserQueryHandler : IRequestHandler<UserQuery, string>
{
    public Task<string> Handle(UserQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Just User!");
    }
}
