using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.TimeTable.Commands.CreateTimeTable;
using ilmV3.Application.TimeTable.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.TimeTable.Commands;

public class CreateTimeTableTests
{
    private readonly IMediator  _mediator;
    private readonly ITimeTableRepository  _timeTableRepository;
    private readonly ServiceProvider _provider;
    
    public CreateTimeTableTests()
    {
        var services = new ServiceCollection();
        _timeTableRepository = A.Fake<ITimeTableRepository>();
        services.AddSingleton(_timeTableRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(CreateTimeTableCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldCreateAndRetrieveTimeTable()
    {
        //Arrange
        TimeTableDto timeTableDto = new TimeTableDto
        {
            Name = "semester1",
            TeacherId = 1,
            SubjectId = 1,
            Audience = "205",
            StudentGroupId = 1,
            Date = DateOnly.MaxValue,
            Time = TimeOnly.MinValue,
            WeekDay = DayOfWeek.Monday
        };
        TimeTableEntity timeTable = new TimeTableEntity
        {
            Name = "semester1",
            TeacherId = 1,
            SubjectId = 1,
            Audience = "205",
            StudentGroupId = 1,
            Date = DateOnly.MaxValue,
            Time = TimeOnly.MinValue,
            WeekDay = DayOfWeek.Monday
        };
        var command = new CreateTimeTableCommand(timeTableDto);
        A.CallTo(()=> _timeTableRepository.CreateTimeTableAsync(A<TimeTableEntity>.Ignored, CancellationToken.None)).Returns(timeTable);
        
        //Act 
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TimeTableVM>();
        result.Name.Should().Be(timeTableDto.Name);
        A.CallTo(()=>_timeTableRepository.CreateTimeTableAsync(A<TimeTableEntity>.That.Matches(t=>
            t.Name == timeTable.Name &&
            t.StudentGroupId == timeTable.StudentGroupId &&
            t.TeacherId == timeTable.TeacherId), A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }
    
    [OneTimeTearDown]
    public void TearDown() => _provider.Dispose();
}
