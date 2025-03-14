using ilmV3.Application.Subject.Commands.CreateSubject;
using ilmV3.Application.Subject.Commands.DeleteSubject;
using ilmV3.Application.Subject.Commands.UpdateSubject;
using ilmV3.Application.Subject.Queries;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ilmV3.Web.Endpoints;

public class Subjects : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapGet(GetSubjects)
           .MapGet(GetSubject, "{subjectId}")
           .MapGet(GetStudentsBySubjectId, "students/{subjectId}")
           .MapGet(GetGroupBySubject, "group/{subjectId}")
           .MapGet(GetTeacherBySubject, "{subjectId}/teacher")
           .MapGet(GetSubjectByTeacher, "{teacherId}/subject")
           .MapPost(CreateSubject)
           .MapPut(UpdateSubject, "{subjectId}")
           .MapDelete(DeleteSubject, "{subjectId}");
    }

    public  async Task<IResult> GetSubjects(ISender _sender)
    {
        var result = await _sender.Send(new GetSubjectsQuery());
        return TypedResults.Ok(result);
    }
    public async Task<IResult> GetSubject(ISender _sender, int subjectId)
    {
        var result = await _sender.Send(new GetSubjectQuery(subjectId));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> CreateSubject(ISender _sender, SubjectDto subject)
    {
        var result = await _sender.Send(new CreateSubjectCommand(subject));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> UpdateSubject(ISender _sender, int subjectId, SubjectDto subject)
    {
        var result = await _sender.Send(new UpdateSubjectCommand(subjectId, subject));
        return TypedResults.Ok(result);
    }
    public async Task<NoContent> DeleteSubject(ISender _sender, int subjectId)
    {
        await _sender.Send(new DeleteSubjectCommand(subjectId));
        return TypedResults.NoContent();
    }
    public async Task<IResult> GetStudentsBySubjectId(ISender _sender, int subjectId)
    {
        var result = await _sender.Send(new GetStudentsBySubjectIdQuery(subjectId));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> GetGroupBySubject(ISender _sender, int subjectId)
    {
        var result = await _sender.Send(new GetGroupBySubjectQuery(subjectId));
        return TypedResults.Ok(result);
    }  
    public async Task<IResult> GetTeacherBySubject(ISender _sender, int subjectId)
    {
        var result = await _sender.Send(new GetTeacherBySubjectQuery(subjectId));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> GetSubjectByTeacher(ISender _sender, int teacherId)
    {
        var result = await _sender.Send(new GetSubjectByTeacherQuery(teacherId));
        return TypedResults.Ok(result);
    }
}
