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

public class GetSubjectByTeacherTests
{
    private readonly IMediator  _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetSubjectByTeacherTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetSubjectByTeacherQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnSubject()
    {
        //Arrange
        var teacherId = 5;
        SubjectEntity subject = new SubjectEntity
        {
            Name = "Test Subject",
            TeacherId = teacherId
        };
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();
        var query = new GetSubjectByTeacherQuery(teacherId);
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
