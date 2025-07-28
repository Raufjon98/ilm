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

public class GetTimeTableTests
{
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetTimeTableTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        services.AddScoped<IAplicationDbContext>(_=> _context);
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(GetTimeTableQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnTimeTable()
    {
        //Arrange
        var timeTableId = 6;
        TimeTableEntity timeTable = new TimeTableEntity
        {
            Id = timeTableId,
            Name = "semester1",
            TeacherId = 1,
            SubjectId = 1,
            Audience = "205",
            StudentGroupId = 1,
            Date = DateOnly.MaxValue,
            Time = TimeOnly.MinValue,
            WeekDay = DayOfWeek.Monday
        };
        await _context.TimeTables.AddAsync(timeTable);
        await _context.SaveChangesAsync();
        var query = new GetTimeTableQuery(timeTableId);
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(timeTableId);
        result.Name.Should().Be("semester1");
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _context.Dispose();
        _provider.Dispose();
    }
}
