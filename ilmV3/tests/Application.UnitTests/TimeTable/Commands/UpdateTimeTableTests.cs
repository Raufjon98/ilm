using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.TimeTable.Commands.UpdateTmeTable;
using ilmV3.Application.TimeTable.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.TimeTable.Commands;

public class UpdateTimeTableTests
{
    private readonly IMediator _mediator;
    private readonly ITimeTableRepository _timeTableRepository;
    private readonly ServiceProvider _provider;

    public UpdateTimeTableTests()
    {
        var services = new ServiceCollection();
        _timeTableRepository = A.Fake<ITimeTableRepository>();
        services.AddSingleton(_timeTableRepository);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateTimeTableCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveTimeTable()
    {
        //Arrange
        var timeTableId = 1;
        TimeTableDto? timeTableDto = new TimeTableDto
        {
            Name = "semester1",
            TeacherId = 12,
            SubjectId = 12,
            Audience = "212",
            StudentGroupId = 1,
            Date = DateOnly.MaxValue,
            Time = TimeOnly.MinValue,
            WeekDay = DayOfWeek.Friday
        };
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
        var command = new UpdateTimeTableCommand(timeTableId, timeTableDto);
        A.CallTo(() => _timeTableRepository.GetTimeTableByIdAsync(timeTableId)).Returns(timeTable);
        A.CallTo(() => _timeTableRepository.UpdateTimeTableAsync(A<TimeTableEntity>._, CancellationToken.None))
            .Returns(timeTable);

        //Act
        var result = await _mediator.Send(command);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TimeTableVM>();
        result!.Id.Should().Be(timeTableId);
        A.CallTo(() => _timeTableRepository.GetTimeTableByIdAsync(timeTableId)).MustHaveHappened();
        A.CallTo(() => _timeTableRepository.UpdateTimeTableAsync(A<TimeTableEntity>.That.Matches(t =>
            t.Name== timeTableDto.Name &&
            t.StudentGroupId == timeTableDto.StudentGroupId), A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown() => _provider.Dispose();
}
