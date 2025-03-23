using ilmV3.Application.Common.Security;

namespace ilmV3.Application.Account.Queries;

[Authorize(Roles = "Teacher")]
public record TeacherQuery : IRequest<string>;

public class TeacherQueryHandler : IRequestHandler<TeacherQuery, string>
{
    public Task<string> Handle(TeacherQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Only for Teachers!");
    }
}
