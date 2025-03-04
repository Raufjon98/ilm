using System.Runtime.CompilerServices;
using ilmV3.Application.TimeTable.Commands.CreateTimeTable;
using ilmV3.Application.TimeTable.Commands.DeleteTimeTable;
using ilmV3.Application.TimeTable.Commands.UpdateTmeTable;
using ilmV3.Application.TimeTable.Queries;
using ilmV3.Domain.Entities;

namespace ilmV3.Web.Endpoints;

public class TimeTables : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapGet(GetTimeTables)
           .MapGet(GetTimeTable, "{timeTableId}")
           .MapPost(CreateTimeTable)
           .MapPut(UpdateTimeTable, "{timeTableId}")
           .MapDelete(DeleteTimeTable, "{timeTableId}");
    }

    public async Task<IResult> GetTimeTables(ISender _sender)
    {
        var result = await _sender.Send(new GetTimeTablesQuery());
        return TypedResults.Ok(result);
    }
    public async Task<IResult> GetTimeTable(ISender _sender, int timeTableId)
    {
        var result = await _sender.Send(new GetTimeTableQuery(timeTableId));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> CreateTimeTable(ISender _sender, TimeTableDto timeTable)
    {
        var result = await _sender.Send(new CreateTimeTableCommand(timeTable));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> UpdateTimeTable(ISender _sender, int timeTableId, TimeTableDto timeTable)
    {
        var result = await _sender.Send(new UpdateTimeTableCommand(timeTableId, timeTable));    
        return TypedResults.Ok(result);
    }
    public async Task<IResult> DeleteTimeTable(ISender _sender, int timeTableId)
    {
        var result = await _sender.Send(new DeleteTimeTableCommand(timeTableId));
        return TypedResults.Ok(result);
    }
}
