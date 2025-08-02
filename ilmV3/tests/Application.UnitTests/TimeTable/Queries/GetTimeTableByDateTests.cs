using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.TimeTable.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.TimeTable.Queries;

public class GetTimeTableByDateTests
{
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;   
    private readonly ServiceProvider _provider;

    public GetTimeTableByDateTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetTimeTableByDateQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnTimeTableByDate()
    {
        //Arrange 
        var date = DateOnly.MaxValue;
        TimeTableEntity timeTable = new TimeTableEntity
        {
            Id = 1,
            Date = date,
            Audience = "403",
            Name = "TimeTableByDateShowing!",
            StudentGroupId = 4,
            SubjectId = 4,
            TeacherId = 3,
        };
        _context.TimeTables.Add(timeTable);
        await _context.SaveChangesAsync();
        
        var query = new GetTimeTableByDateQuery(date);
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TimeTableVM>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
