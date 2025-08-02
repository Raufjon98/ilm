using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.StudentGroup.Queries;

public class GetStudentGroupByStudentTests
{
    private readonly IMediator  _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetStudentGroupByStudentTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetStudentGroupByStudentQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnStudentsGroup()
    {
        //Arrange
        var studentId = 4;
        var subjectId = 3;
        StudentEntity student = new StudentEntity { Id = studentId, Name = "Mufaso" };
        
        _context.Students.Add(student);
        _context.StudentGroups.AddRange(
            new StudentGroupEntity { SubjectId = subjectId, Name = "Geography", Students = new List<StudentEntity>{ student }}, 
            new StudentGroupEntity { SubjectId = subjectId, Name = "IT networks", Students = new List<StudentEntity>{ student } });
        await _context.SaveChangesAsync();
        var query = new GetStudentGroupByStudentQuery(studentId);
        
        //Act
        var result = await _mediator.Send(query);
        
        //Asert
        IEnumerable<StudentGroupVM> studentGroupVms = result.ToList();
        studentGroupVms.Should().NotBeNull();
        studentGroupVms.Should().BeOfType<List<StudentGroupVM>>();
        studentGroupVms.Should().Contain(sg=>sg.SubjectId == subjectId);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}

