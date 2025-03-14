

using ilmV3.Application.Grade.Commands.CreateGrade;
using ilmV3.Application.Grade.Commands.DeleteGrade;
using ilmV3.Application.Grade.Commands.UpdateGrade;
using ilmV3.Application.Grade.Queries;

namespace ilmV3.Web.Endpoints;

public class Grades : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapGet(GetGrades)
           .MapGet(GetGrade, "{gradeId}")
           .MapPost(CreateGrade)
           .MapPut(UpdateGrade, "{gradeId}")
           .MapDelete(DeleteGrade, "{gradeId}");
    }

    public async Task<IResult> GetGrades(ISender _sender)
    {
        var result = await _sender.Send(new GetGradesQuery());
        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetGrade(ISender _sender, int gradeId)
    {
        var result = await _sender.Send(new GetGradeQuery(gradeId));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> CreateGrade(ISender _sender, GradeDto grade)
    {
        var result = await _sender.Send(new CreateGradeCommand(grade));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> UpdateGrade(ISender _sender, int gradeId, GradeDto grade)
    {
        var result = await _sender.Send(new UpdateGradeCommand(grade, gradeId));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> DeleteGrade(ISender _sender, int gradeId)
    {
        var result = await _sender.Send(new DeleteGradeCommand(gradeId));
        return TypedResults.Ok(result);
    }
}
