using ilmV3.Application.Account.Commands.Login;
using ilmV3.Application.Account.Commands.Register;

namespace ilmV3.Web.Endpoints;

public class Account : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(StudentRegister, "/student-register")
            .MapPost(AdminRegister, "/admin-register")
            .MapPost(TeacherRegister, "/teacher-register")
            .MapPost(Login, "/login");
    }
    public async Task<IResult> StudentRegister(ISender _sender, RegisterDto register)
    {
        var result = await _sender.Send(new StudentRegisterCommand(register));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> AdminRegister(ISender _sender, RegisterDto register)
    {
        var result = await _sender.Send(new AdminRegisterCommand(register));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> TeacherRegister(ISender _sender, RegisterDto register)
    {
        var result = await _sender.Send(new TeacherRegisterCommand(register));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> Login(ISender _sender, LoginDto login)
    {
        var result = await _sender.Send(new LoginCommand(login));
        return TypedResults.Ok(result);
    }
}
