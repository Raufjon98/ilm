
using ilmV3.Application.Common.Security;

namespace ilmV3.Application.Account.Queries;

[Authorize(Roles = "Student")]

public record StudentQuery : IRequest<string>;

public class StudentQuertyHandler : IRequestHandler<StudentQuery, string>
{
    public Task<string> Handle(StudentQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Only for Students!");
    }
}
