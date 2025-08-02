using ilmV3.Application.Account.Commands.Register;
using ilmV3.Application.Student.Commands.DeleteStudent;
using ilmV3.Application.Student.Commands.UpdateStudent;
using ilmV3.Application.Student.Commands.UpdateStudentGroup;
using ilmV3.Application.Student.Queries;

namespace ilmV3.Web.Endpoints;

public class Students : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .RequireAuthorization()
           .MapGet(GetStudents)
           .MapGet(GetExcellentStudents, "/excellents")
           .MapGet(GetStudent, "{studentId}")
           .MapDelete(DeleteStudent, "{studentId}")
           .MapPut(UpdateStudentGroupForStudent, "groupEdit/{studentId}/{studentGroupId}")
           .MapPut(UpdateStudent, "{studentId}");
    }

    public async Task<IResult> GetStudents(ISender _sender)
    {
        var result = await _sender.Send(new GetStudentsQuery());
        return TypedResults.Ok(result);
    }
    public async Task<IResult> GetStudent(ISender _sender, int studentId)
    {
        var result = await _sender.Send(new GetStudentQuery(studentId));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> UpdateStudent(ISender _sender, StudentDto student, string studentId)
    {
        var result = await _sender.Send(new UpdateStudentCommand(studentId, student));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> DeleteStudent(ISender _sender, int studentId)
    {
        var result = await _sender.Send(new DeleteStudentCommand(studentId));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> GetExcellentStudents(ISender _sender)
    {
        var result = await _sender.Send(new GetExcellentStudentsQuery());
        return TypedResults.Ok(result);
    }
    public async Task<IResult> UpdateStudentGroupForStudent(ISender _sender, int studentId, int StudentGroupId)
    {
        var result = await _sender.Send(new UpdateStudentsGroupCommand(studentId, StudentGroupId));
        return TypedResults.Ok(result);
    }
}
