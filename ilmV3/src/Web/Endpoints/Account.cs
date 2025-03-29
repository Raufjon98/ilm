using ilmV3.Application.Account.Commands.Login;
using ilmV3.Application.Account.Commands.Register;
using ilmV3.Application.Account.Queries;
using ilmV3.Application.Common.Security;

namespace ilmV3.Web.Endpoints;

public class Account : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(Register, "/register")
            .MapPost(Login, "/login")
            .MapGet(Teacher, "/policy/teacher")
            .MapGet(Admin, "/policy/Admin")
            .MapGet(Student, "/policy/student");
    }
    public async Task<IResult> Register(ISender _sender, RegisterDto register)
    {
        var result = await _sender.Send(new RegisterCommand(register));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> Login(ISender _sender, LoginDto login)
    {
        var result = await _sender.Send(new LoginCommand(login));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> Teacher(ISender _sender)
    {
        var result = await _sender.Send(new TeacherQuery());
        return TypedResults.Ok(result);
    }

    public async Task<IResult> Admin(ISender _sender)
    {
        var result = await _sender.Send(new AdminQuery());
        return TypedResults.Ok(result);
    }

    public async Task<IResult> Student(ISender _sender)
    {
        var result = await _sender.Send(new StudentQuery());
        return TypedResults.Ok(result);
    }
}
