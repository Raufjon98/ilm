using ilmV3.Application.Common.Security;

namespace ilmV3.Application.Account.Queries;

[Authorize(Roles = "Administrator")]
public record AdminQuery : IRequest<string>;

public class AdminQueryHandler : IRequestHandler<AdminQuery, string>
{
    public Task<string> Handle(AdminQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Only for Administrator!");
    }
}
