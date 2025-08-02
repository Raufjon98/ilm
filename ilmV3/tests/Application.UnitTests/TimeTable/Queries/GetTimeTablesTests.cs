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

public class GetTimeTablesTests
{
    private readonly IMediator  _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetTimeTablesTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_=> _context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetTimeTablesQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnAllTimeTables()
    {
        //Arrange
        _context.TimeTables.AddRange(new List<TimeTableEntity>()
        {
            new TimeTableEntity
            {
                Name = "semester1",
                TeacherId = 1,
                SubjectId = 1,
                Audience = "205",
                StudentGroupId = 1,
                Date = DateOnly.MaxValue,
                Time = TimeOnly.MinValue,
                WeekDay = DayOfWeek.Monday
            },
            new TimeTableEntity
            {
                Name = "semester2",
                TeacherId = 1,
                SubjectId = 1,
                Audience = "205",
                StudentGroupId = 1,
                Date = DateOnly.MaxValue,
                Time = TimeOnly.MinValue,
                WeekDay = DayOfWeek.Monday
            },
            new TimeTableEntity
            {
                Name = "semester3",
                TeacherId = 1,
                SubjectId = 1,
                Audience = "205",
                StudentGroupId = 1,
                Date = DateOnly.MaxValue,
                Time = TimeOnly.MinValue,
                WeekDay = DayOfWeek.Monday
            }
        });
        await _context.SaveChangesAsync();
        var query = new GetTimeTablesQuery();
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        IEnumerable<TimeTableVM> timeTableVms = result.ToList();
        timeTableVms.Should().NotBeNull();
        timeTableVms.Should().BeOfType<List<TimeTableVM>>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _context.Dispose();
        _provider.Dispose();
    }
}
