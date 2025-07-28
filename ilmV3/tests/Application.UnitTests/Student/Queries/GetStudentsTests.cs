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

public class GetStudentsTests
{
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetStudentsTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetStudentsQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnAllStudents()
    {
        //Arrange
        _context.AddRange(new List<StudentEntity>()
        {
            new StudentEntity { Id = 1, Name = "John Smith", },
            new StudentEntity { Id = 2, Name = "John Doe", },
            new StudentEntity { Id = 3, Name = "John Jones", },
            new StudentEntity { Id = 4, Name = "John Jones Junior", }
        });
        await _context.SaveChangesAsync();
        var query = new GetStudentsQuery();

        //Act
        var result = await _mediator.Send(query);

        //Assert
        result.Should().NotBeNull();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _context.Dispose();
        _provider.Dispose();
    }
}
