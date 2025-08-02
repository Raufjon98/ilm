using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Student.Queries;

public class GetStudentTests
{
    private readonly ApplicationDbContext _context;
    private readonly IMediator  _mediator;
    private readonly ServiceProvider _provider;

    public GetStudentTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(GetStudentQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnStudent()
    {
        //Arrange
        var studentId = 22;
        StudentEntity student = new StudentEntity { Id = studentId, Name = "Mufaso"};
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        var query = new GetStudentQuery(studentId);
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(studentId);
        result.Name.Should().Be(student.Name);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
