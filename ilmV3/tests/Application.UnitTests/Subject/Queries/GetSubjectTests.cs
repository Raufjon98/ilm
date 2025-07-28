using AutoMapper;
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

public class GetSubjectTests
{
    private readonly IMediator  _mediator;
    private readonly ServiceProvider  _provider;
    private readonly ApplicationDbContext _context;
    
    public GetSubjectTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetSubjectQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldGetSubject()
    {
        //Arrange
        var subjectId = 7;
        SubjectEntity subject =new SubjectEntity{Id = subjectId, Name = "Test", TeacherId = 6};
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();
        var query = new GetSubjectQuery(subjectId);
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<SubjectVM>();
        result.Id.Should().Be(subjectId);
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _context.Dispose();
        _provider.Dispose();
    }
}
