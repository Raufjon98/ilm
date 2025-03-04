﻿using ilmV3.Application.Student.Commands.CreateStudent;
using ilmV3.Application.Student.Commands.DeleteStudent;
using ilmV3.Application.Student.Commands.UpdateStudent;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.Entities;

namespace ilmV3.Web.Endpoints;

public class Students : EndpointGroupBase
{

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapGet(GetStudents)
           .MapGet(GetStudent, "{studentId}")
           .MapPost(CreateStudent)
           .MapDelete(DeleteStudent, "{studentId}")
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
    public async Task<IResult> CreateStudent(ISender _sender, StudentDto student, string email, string password)
    {
        var result = await _sender.Send(new CreateStudentCommand(student, email, password));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> UpdateStudent(ISender _sender, StudentDto student, int studentId)
    {
        var result = await _sender.Send(new UpdateStudentCommand(studentId, student));
        return TypedResults.Ok(result);
    }
    public async Task<IResult> DeleteStudent(ISender _sender, int studentId)
    {
        var result = await _sender.Send(new DeleteStudentCommand(studentId));
        return TypedResults.Ok(result);
    }
}
