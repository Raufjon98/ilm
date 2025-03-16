
using ilmV3.Application.Account.Register;

namespace ilmV3.Web.Endpoints;

public class Account : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(Register);
    }
    public async Task<IResult> Register(ISender _sender, RegisterDto register)
    {
        var result = await _sender.Send(new RegisterCommand(register));
        return TypedResults.Ok(result);
    }
}
