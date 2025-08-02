using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Subject.Commands.DeleteSubject;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Subject.Commands;

public class DeleteSubjectTests
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IMediator _mediator;
    private readonly ServiceProvider _provider;

    public DeleteSubjectTests()
    {
        var services = new ServiceCollection();
        _subjectRepository = A.Fake<ISubjectRepository>();
        services.AddSingleton(_subjectRepository);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteSubjectCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldDeleteSubject()
    {
        //Arrange
        var subjectId = 5;
        var command = new DeleteSubjectCommand(subjectId);
        SubjectEntity? subject = new SubjectEntity {Id = subjectId, Name = "Korean for Russians", TeacherId = 6};
        A.CallTo(()=> _subjectRepository.GetSubjectByIdAsync(subjectId)).Returns(subject);
        A.CallTo(() => _subjectRepository.DeleteSubjectAsync(subject, CancellationToken.None)).Returns(true);
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().BeTrue();
        A.CallTo(()=> _subjectRepository.GetSubjectByIdAsync(subjectId)).MustHaveHappened();
        A.CallTo(() => _subjectRepository.DeleteSubjectAsync(A<SubjectEntity>.That.Matches(s=>
            s.Id == subjectId &&
            s.Name == subject.Name),  CancellationToken.None)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }
}
