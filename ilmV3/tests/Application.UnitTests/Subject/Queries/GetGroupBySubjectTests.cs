using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Application.Subject.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Subject.Queries;

public class GetGroupBySubjectTests
{
    private readonly IMediator  _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetGroupBySubjectTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_=>_context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetGroupBySubjectQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnGroupBySubject()
    {
        //Arrange
        var subjectId = 1;
        StudentGroupEntity studentGroupEntity = new StudentGroupEntity
        {
            Name = "English for boys",
            SubjectId = subjectId,
            CodeName = "1-54-55-bb",
            TeacherId = 1
        };
        _context.StudentGroups.Add(studentGroupEntity);
        await _context.SaveChangesAsync();
        var query = new GetGroupBySubjectQuery(subjectId);
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StudentGroupVM>();
        result.SubjectId.Should().Be(subjectId);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _context.Dispose();
        _provider.Dispose();
    }
}
