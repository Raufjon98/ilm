using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Subject.Commands.CreateSubject;
using ilmV3.Application.Subject.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Subject.Commands;

public class CreateSubjectTests
{
    private readonly IMediator _mediator;
    private readonly ISubjectRepository _subjectRepository;
    private readonly ServiceProvider _provider;

    public CreateSubjectTests()
    {
        var services = new ServiceCollection();
        _subjectRepository = A.Fake<ISubjectRepository>();
        services.AddSingleton(_subjectRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(CreateSubjectCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldCreateAndRetrieveSubject()
    {
        //Arrange
        SubjectDto subjectDto = new SubjectDto { Name = "Russian", TeacherId = 1 };
        SubjectEntity subject = new SubjectEntity { Name = "Russian", TeacherId = 1 };
        var command = new CreateSubjectCommand(subjectDto);
        A.CallTo(() => _subjectRepository.CreateSubjectAsync(subject, CancellationToken.None)).Returns(subject);
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(subject.Id);
        A.CallTo(()=> _subjectRepository.CreateSubjectAsync(A<SubjectEntity>.That.Matches(s=>s.Name == subject.Name), CancellationToken.None)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }
}
