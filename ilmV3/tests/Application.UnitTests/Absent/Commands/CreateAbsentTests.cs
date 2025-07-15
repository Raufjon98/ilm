using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Absent.Commands.CreateAbsent;
using ilmV3.Application.Absent.Queries.GetAbsent;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Absent.Commands;
public class CreateAbsentTests
{
    private readonly IMediator _mediator;
    private readonly ServiceProvider _provider;
    private readonly IAbsentRepository _absentRepository;
    public CreateAbsentTests()
    {
        var services = new ServiceCollection();
        _absentRepository = A.Fake<IAbsentRepository>();
        var context = A.Fake<IAplicationDbContext>();
        services.AddSingleton(context);
        services.AddSingleton(_absentRepository);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAbsentCommandHandle).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldCreateAndRetrieveAbsent()
    {
        //Arrange
        AbsentDto absentCreate = new AbsentDto
        {
            Date = DateOnly.FromDayNumber(1),
            Absent = true,
            ClassDay = "day1",
            StudentId = 1,
            SubjectId = 2,
            TeacherId = 5
        };
        AbsentEntity absent = new AbsentEntity
        {
            Date = DateOnly.FromDayNumber(1),
            Absent = true,
            ClassDay = "day1",
            StudentId = 1,
            SubjectId = 2,
            TeacherId = 5
        };
        A.CallTo(() => _absentRepository.CreateAbsentAsync(A<AbsentEntity>._, default)).Returns(absent);
        var command = new CreateAbsentCommand(absentCreate);

        //Act
        var result = await _mediator.Send(command);

        //Assert
        result.Should().NotBeNull();
        result.StudentId.Should().Be(absentCreate.StudentId);
        result.Absent.Should().Be(absentCreate.Absent);
        A.CallTo(() => _absentRepository.CreateAbsentAsync(A<AbsentEntity>._, default)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider?.Dispose();
    }
}
