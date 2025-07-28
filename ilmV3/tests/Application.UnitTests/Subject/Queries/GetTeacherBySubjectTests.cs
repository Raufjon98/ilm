using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Subject.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Subject.Queries;

public class GetTeacherBySubjectTests
{
    private readonly IMediator _mediator; 
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetTeacherBySubjectTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetTecherBySubjectQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnteacgerBySubject()
    {
        //Arrange
        var subjectId = 5;
        var teacherId = 1;
        TeacherEntity? teacher = new TeacherEntity{ Id = teacherId,Name = "John Doe"};
        SubjectEntity subject = new SubjectEntity { Id = subjectId, TeacherId = teacherId };
        _context.Subjects.Add(subject);
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
        var query = new GetTeacherBySubjectQuery(subjectId);
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(teacherId);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
