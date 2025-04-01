using ilmV3.Application.Admin.Commands.DeleteAdmin;
using ilmV3.Application.Admin.Commands.UpdateAdmin;
using ilmV3.Application.Admin.Queries;

namespace ilmV3.Web.Endpoints;

public class Admins : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapDelete(DeleteAdmin, "{adminId}")
            .MapPut(UpdateAdmin, "{adminId}")
            .MapGet(GetAdmins)
            .MapGet(GetAdmin, "{adminId}");
    }

    public async Task<IResult> UpdateAdmin(ISender _sender, string adminId, AdminDto adminDto)
    {
        var result = await _sender.Send(new UpdateAdminCommand(adminId, adminDto));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> DeleteAdmin(ISender _sender, string adminId)
    {
        var result = await _sender.Send(new DeleteAdminCommand(adminId));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetAdmins(ISender _sender)
    {
        var result = await _sender.Send(new GetAdminsQuery());
        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetAdmin(ISender _sender, string adminId)
    {
        var result = await _sender.Send(new GetAdminQuery(adminId));
        return TypedResults.Ok(result);
    }
}
