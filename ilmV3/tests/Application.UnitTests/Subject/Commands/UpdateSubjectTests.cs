using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Subject.Commands.UpdateSubject;
using ilmV3.Application.Subject.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Subject.Commands;

public class UpdateSubjectTests
{
    private readonly IMediator  _mediator;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ServiceProvider _provider;

    public UpdateSubjectTests()
    {
        var services = new ServiceCollection();
        _subjectRepository = A.Fake<ISubjectRepository>();
        services.AddSingleton(_subjectRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(UpdateSubjectCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveSubject()
    {
        //Arrange
        var subjectId = 5;
        var command = new UpdateSubjectCommand(subjectId, new SubjectDto{Name = "subject For Update command", TeacherId = 1});
        SubjectEntity subject = new SubjectEntity{Id = subjectId, Name = "subject for repository", TeacherId = 3};
        A.CallTo(()=> _subjectRepository.GetSubjectByIdAsync(subjectId)).Returns(subject);
        A.CallTo(()=> _subjectRepository.UpdateSubjectAsync(A<SubjectEntity>._, CancellationToken.None)).Returns(subject);
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(subjectId);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }
}
