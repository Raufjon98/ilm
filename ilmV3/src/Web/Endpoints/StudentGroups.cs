using ilmV3.Application.StudentGroup.Commands.CreateStudentGroup;
using ilmV3.Application.StudentGroup.Commands.DeleteStudentGroup;
using ilmV3.Application.StudentGroup.Commands.UpdateStudentGroup;
using ilmV3.Application.StudentGroup.Queries;

namespace ilmV3.Web.Endpoints;

public class StudentGroups : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapGet(GetStudentGroups)
           .MapGet(GetStudentGroup, "{studentGroupid}")
           .MapGet(GetStudentGroupMembers, "members/{studentGroupid}")
           .MapGet(GetStudentGroupByStudent, "/groups{studentId}")
           .MapGet(GetTeacherByStudentGroup, "{studentGroupId}/teacher")
           .MapPost(CreateStudenGroup)
           .MapPut(UpdateStudentGroup, "{studentGroupId}")
           .MapDelete(DeleteStudentGroup, "{studentGroupId}");
    }

    public async Task<IResult> GetStudentGroups(ISender _sender)
    {
        var result = await _sender.Send(new GetStudentGroupsQuery());
        return TypedResults.Ok(result);
    }
    public async Task<IResult> GetStudentGroup(ISender _sender, int studentGroupid)
    {
        var result = await _sender.Send(new GetStudentGroupQuery(studentGroupid));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> CreateStudenGroup(ISender _sender, StudentGroupDto studentGroup)
    {
        var result = await _sender.Send(new CreateStudentGroupCommand(studentGroup));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> UpdateStudentGroup(ISender _sender, StudentGroupDto studentGroup, int studentGroupid)
    {
        var result = await _sender.Send(new UpdateStudentGroupCommand(studentGroupid, studentGroup));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> DeleteStudentGroup(ISender _sender, int studentGroupId)
    {
        var result = await _sender.Send(new DeleteStudentGroupCommand(studentGroupId));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> GetStudentGroupMembers(ISender _sender, int studentGroupid)
    {
        var result = await _sender.Send(new GetStudentGroupMembersQuery(studentGroupid));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> GetStudentGroupByStudent(ISender _sender, int studentId)
    {
        var result = await _sender.Send(new GetStudentGroupByStudentQuery(studentId));
        return TypedResults.Ok(result);
    }

    public async Task<IResult> GetTeacherByStudentGroup(ISender _sender, int studentGroupId)
    {
        var result = await _sender.Send(new GetTeacherByStudentGroupQuery(studentGroupId));
        return TypedResults.Ok(result);
    }
}
