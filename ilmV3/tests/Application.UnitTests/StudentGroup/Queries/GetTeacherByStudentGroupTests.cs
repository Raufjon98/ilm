using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.StudentGroup.Queries;

public class GetTeacherByStudentGroupTests
{
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetTeacherByStudentGroupTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(GetTeacherByStudentGroupQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnTeacherByStudentGroup()
    {
        //Arrange
        var studentGroupId = 1;
        var teacherId = 1;
        StudentGroupEntity studentGroup = new StudentGroupEntity
        {
            Id = studentGroupId, TeacherId = teacherId, Name = "Korean for Russians",
        };
        TeacherEntity teacher = new TeacherEntity { Id = teacherId, Name = "Olimov", StudentGroups = new List<StudentGroupEntity>{ studentGroup } };
        _context.Teachers.Add(teacher);
        _context.StudentGroups.Add(studentGroup);
        await _context.SaveChangesAsync();
        var query = new GetTeacherByStudentGroupQuery(studentGroupId);
        
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TeacherVM>();
        result.Id.Should().Be(teacherId);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
