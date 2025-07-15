using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Absent.Commands.UpdateAbsent;
using ilmV3.Application.Absent.Queries.GetAbsent;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Absent.Commands;
public class UpdateAbsentTests
{
    private readonly IMediator _mediator;
    private readonly ServiceProvider _provider;
    private readonly IAbsentRepository _absentRepository;

    public UpdateAbsentTests()
    {
        var services = new ServiceCollection();
        _absentRepository = A.Fake<IAbsentRepository>();
        var context = A.Fake<IAplicationDbContext>();
        services.AddSingleton(context);
        services.AddSingleton(_absentRepository);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateAbsentCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveAbsent()
    {
        //Arrange
        var absentId = 4;
        AbsentDto absentUpdate = new AbsentDto
        {
            Date = DateOnly.FromDayNumber(1),
            Absent = false,
            ClassDay = "Test Day",
            StudentId = 2,
            SubjectId = 3,
            TeacherId = 4,
        };
        AbsentEntity absent = new AbsentEntity
        {
            Date = DateOnly.FromDayNumber(1),
            Absent = false,
            ClassDay = "Test Day",
            StudentId = 2,
            SubjectId = 3,
            TeacherId = 4,
        };
        A.CallTo(() => _absentRepository.GetAbsentByIdAsync(absentId));
        A.CallTo(() => _absentRepository.UpdateAbsentAsync(absent, default));
        var command = new UpdateAbsentCommand(absentId, absentUpdate);

        //Act
        var result = await _mediator.Send(command);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<AbsentVM>();
        result.Absent.Should().BeFalse();
        A.CallTo(() => _absentRepository.GetAbsentByIdAsync(absentId)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _absentRepository.UpdateAbsentAsync(A<AbsentEntity>.That.Matches(a =>
        a.Absent == false &&
        a.StudentId == absent.StudentId &&
        a.SubjectId == absent.SubjectId &&
        a.TeacherId == absent.TeacherId), A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider?.Dispose();
    }
}
