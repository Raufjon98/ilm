using System.Runtime.CompilerServices;
using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Absent.Commands.DeleteAbsent;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Absent.Commands;
public class DeleteAbsentTests
{
    private readonly IMediator _mediator;
    private readonly ServiceProvider _provider;
    private readonly IAbsentRepository _absentRepository;

    public DeleteAbsentTests()
    {
        var services = new ServiceCollection();
        _absentRepository = A.Fake<IAbsentRepository>();
        var context = A.Fake<IAplicationDbContext>();
        services.AddSingleton(context);
        services.AddSingleton(_absentRepository);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteAbsentCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldDeleteAbsentAntReturnTrue()
    {
        //Arrange
        var absentId = 4;
        AbsentEntity absent = new AbsentEntity { Id = absentId };
        var command = new DeleteAbsentCommand(absentId);
        A.CallTo(() => _absentRepository.GetAbsentByIdAsync(absentId)).Returns(absent);
        A.CallTo(() => _absentRepository.DeleteAbsentAsync(A<AbsentEntity>._, default)).Returns(true);

        //Act
        var result = await _mediator.Send(command);

        //Assert
        result.Should().BeTrue();
        A.CallTo(() => _absentRepository.GetAbsentByIdAsync(absentId)).MustHaveHappened();
        A.CallTo(() => _absentRepository.DeleteAbsentAsync(A<AbsentEntity>._, A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider?.Dispose();
    }
}
