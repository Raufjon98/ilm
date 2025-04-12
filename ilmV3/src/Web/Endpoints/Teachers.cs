using ilmV3.Application.Account.Commands.Register;
using ilmV3.Application.Teacher.Commands.DeleteTeacher;
using ilmV3.Application.Teacher.Commands.UpdateTeacher;
using ilmV3.Application.Teacher.Queries;

namespace ilmV3.Web.Endpoints;

public class Teachers : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapGet(GetTeachers)
           .MapGet(GetTeacher, "{teacherId}")
           .MapPut(UpdateTeacher, "{teacherId}")
           .MapDelete(DeleteTeacher, "{teacherId}");
    }

    public async Task<IResult> GetTeachers(ISender _sender)
    {
        var result = await _sender.Send(new GetTeachersQuery());
        return TypedResults.Ok(result);
    }
    public async Task<IResult> GetTeacher(ISender _sender, string teacherId)
    {
        var result = await _sender.Send(new GetTeacherQuery(teacherId));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> UpdateTeacher(ISender _sender, string teacherId,  TeacherDto teacher)
    {
        var result = await _sender.Send(new UpdateTeacherCommand(teacherId, teacher));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> DeleteTeacher(ISender _sender, int teacherId)
    {
        var result = await _sender.Send(new DeleteTeacherCommand(teacherId));
        return TypedResults.Ok(result);
    }
}
