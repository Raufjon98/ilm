using ilmV3.Application.Absent.Commands.CreateAbsent;
using ilmV3.Application.Absent.Commands.DeleteAbsent;
using ilmV3.Application.Absent.Commands.UpdateAbsent;
using ilmV3.Application.Absent.Queries.GetAbsent;

namespace ilmV3.Web.Endpoints;

public class Absents : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetAbsents)
            .MapGet(GetAbsentById, "{absentId}")
            .MapPost(CreateAbsent)
            .MapPut(UpdateAbsent, "{absentId}")
            .MapDelete(DeleteAbsent, "{absentId}");
    }
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AbsentEntity>))]
    public async Task<IResult> GetAbsents(ISender _sender)
    {
        var result = await _sender.Send(new GetAbsentsQuery());
        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetAbsentById(ISender _sender, int absentId)
    {
        var result = await _sender.Send(new GetAbsentByIdQuery(absentId));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> CreateAbsent(ISender _sender, AbsentDto absent)
    {
        var result =  await _sender.Send(new CreateAbsentCommand(absent));
        return TypedResults.Ok(result);
    }
     public async Task<IResult> UpdateAbsent(ISender _sender, int id, AbsentDto absent)
    {
        var result = await _sender.Send(new UpdateAbsentCommand(id, absent));
        return TypedResults.Ok(result); 
    }

    public async Task<IResult> DeleteAbsent(ISender _sender, int absentId)
    {
        var result = await _sender.Send(new DeleteAbsentCommand(absentId));
        return TypedResults.Ok(result);
    }
}
