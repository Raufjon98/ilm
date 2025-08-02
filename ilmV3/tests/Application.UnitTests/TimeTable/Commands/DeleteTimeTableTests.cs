using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.TimeTable.Commands.DeleteTimeTable;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.TimeTable.Commands;

public class DeleteTimeTableTests
{
    private readonly IMediator _mediator;
    private readonly ITimeTableRepository _timeTableRepository;
    private readonly ServiceProvider _provider;

    public DeleteTimeTableTests()
    {
        var services = new ServiceCollection();
        _timeTableRepository = A.Fake<ITimeTableRepository>();
        services.AddSingleton(_timeTableRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(DeleteTimeTableCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldDeleteTimeTable()
    {
        //Arrange 
        var timeTableId = 5;
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
        var command = new DeleteTimeTableCommand(timeTableId);
        A.CallTo(() => _timeTableRepository.GetTimeTableByIdAsync(timeTableId)).Returns(timeTable);
        A.CallTo(()=> _timeTableRepository.DeleteTimeTableAsync(timeTable, CancellationToken.None)).Returns(true);
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().BeTrue();
        A.CallTo(()=> _timeTableRepository.GetTimeTableByIdAsync(timeTableId)).MustHaveHappened();
        A.CallTo(()=> _timeTableRepository.DeleteTimeTableAsync(timeTable, CancellationToken.None)).MustHaveHappenedOnceExactly();
    }
    
    [OneTimeTearDown]
    public void TearDown()=> _provider.Dispose();
}
