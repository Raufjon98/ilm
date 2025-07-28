using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.StudentGroup.Queries;

public class GetStudentGroupTests
{
    private readonly IMediator  _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetStudentGroupTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        services.AddScoped<IAplicationDbContext>(_=>_context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetStudentGroupQuery).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnStudentGroup()
    {
        //Arrange 
        var studentGroupId = 1;
        StudentGroupEntity studentGroup = new StudentGroupEntity
        {
            Id = studentGroupId,
            Name = "getTestStudentGroup",
            CodeName = "theCodeName",
            SubjectId = 1,
            TeacherId = 1
        };
        _context.StudentGroups.Add(studentGroup);
        await _context.SaveChangesAsync();
        var query = new GetStudentGroupQuery(studentGroupId);
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(studentGroupId);
        result.Should().BeOfType<StudentGroupVM>();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
