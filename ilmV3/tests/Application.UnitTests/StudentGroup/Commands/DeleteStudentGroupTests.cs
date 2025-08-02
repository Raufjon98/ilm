using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.StudentGroup.Commands.DeleteStudentGroup;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.StudentGroup.Commands;

public class DeleteStudentGroupTests
{
    private readonly IMediator  _mediator;
    private readonly IStudentGroupRepository  _studentGroupRepository;
    private readonly ServiceProvider _provider;

    public DeleteStudentGroupTests()
    {
        var services = new ServiceCollection();
        _studentGroupRepository = A.Fake<IStudentGroupRepository>();
        services.AddSingleton(_studentGroupRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(DeleteStudentGroupCommandHandler).Assembly));        
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShoulDeleteStudentGroup()
    {
        //Arrange
        var studentGroupId = 1;
        var command = new DeleteStudentGroupCommand(studentGroupId);
        StudentGroupEntity studentGroup = new StudentGroupEntity
        {
            Id = studentGroupId, Name = "DeleteTest", CodeName = "DeleteTestCodeName", SubjectId = 1, TeacherId = 1
        };
        A.CallTo(()=> _studentGroupRepository.GetStudentGroupByIdAsync(studentGroupId)).Returns(studentGroup);
        A.CallTo(() => _studentGroupRepository.DeleteStudentGroupAsync(studentGroup, CancellationToken.None)).Returns(true);
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().BeTrue();
        A.CallTo(()=> _studentGroupRepository.GetStudentGroupByIdAsync(studentGroupId)).MustHaveHappened();
        A.CallTo(()=> _studentGroupRepository.DeleteStudentGroupAsync(A<StudentGroupEntity>.That.Matches(g=>g.Id==studentGroupId), A<CancellationToken>._)).MustHaveHappened();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }
}
