using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Student.Queries;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.StudentGroup.Queries;

public class GetStudentGroupMembersTests
{
    private readonly IMediator  _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetStudentGroupMembersTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetStudentGroupMembersQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnAllStudentGroupMembers()
    {
        //Arrange
        var studentGroupId = 1;
        var query = new GetStudentGroupMembersQuery(studentGroupId);
        StudentGroupEntity studentGroup = new StudentGroupEntity
        {
            Id = studentGroupId,
            Students = new List<StudentEntity>
            {
                new StudentEntity{Id = 1, Name = "Jackie"},
                new StudentEntity{Id = 2, Name = "John"}
            }
        };
        _context.StudentGroups.Add(studentGroup);
        await _context.SaveChangesAsync();
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        IEnumerable<StudentVM> studentVms = result.ToList();
        studentVms.Should().NotBeNull();
        studentVms.Should().BeOfType<List<StudentVM>>();
        studentVms.Should().Contain(s=>s.Name == "Jackie");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
