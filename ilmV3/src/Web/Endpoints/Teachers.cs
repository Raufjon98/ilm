using ilmV3.Application.Teacher.Commands.CreateTeacher;
using ilmV3.Application.Teacher.Commands.DeleteTeacher;
using ilmV3.Application.Teacher.Commands.UpdateTeacher;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.Entities;

namespace ilmV3.Web.Endpoints;

public class Teachers : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapGet(GetTeachers)
           .MapGet(GetTeacher, "{teacherId}")
           .MapPost(CreateTeacher)
           .MapPut(UpdateTeacher, "{teacherId}")
           .MapDelete(DeleteTeacher, "{teacherId}");
    }

    public async Task<IResult> GetTeachers(ISender _sender)
    {
        var result = await _sender.Send(new GetTeachersQuery());
        return TypedResults.Ok(result);
    }
    public async Task<IResult> GetTeacher(ISender _sender, int teacherId)
    {
        var result = await _sender.Send(new GetTeacherQuery(teacherId));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> CreateTeacher(ISender _sender, TeacherDto teacher , string email, string password)
    {
        var result = await _sender.Send(new CreateTeacherCommand(teacher, email, password));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> UpdateTeacher(ISender _sender, int teacherId,  TeacherDto teacher)
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
