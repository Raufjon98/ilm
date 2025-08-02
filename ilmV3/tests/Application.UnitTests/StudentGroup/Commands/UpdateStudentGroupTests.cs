using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.StudentGroup.Commands.UpdateStudentGroup;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UpdateStudentGroupCommandHandler = ilmV3.Application.Student.Commands.UpdateStudentGroup.UpdateStudentGroupCommandHandler;

namespace ilmV3.Application.UnitTests.StudentGroup.commands;

public class UpdateStudentGroupTests
{
    private readonly IMediator  _mediator;
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly ServiceProvider _provider;

    public UpdateStudentGroupTests()
    {
        var services = new ServiceCollection();
        _studentGroupRepository = A.Fake<IStudentGroupRepository>();
        services.AddSingleton(_studentGroupRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(UpdateStudentGroupCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveStudentGroup()
    {
        //Arrange 
        var studentGroupId = 1;
        StudentGroupDto studentGroupDto = new StudentGroupDto
        {
            Name = "update", CodeName = "update", SubjectId = 1, TeacherId = 1
        };
        StudentGroupEntity studentGroup = new StudentGroupEntity
        {
           Id = studentGroupId, Name = "update", CodeName = "update", SubjectId = 1, TeacherId = 1
        };
        var command = new UpdateStudentGroupCommand(studentGroupId, studentGroupDto);
        A.CallTo(()=> _studentGroupRepository.GetStudentGroupByIdAsync(studentGroupId)).Returns(studentGroup);
        A.CallTo(()=> _studentGroupRepository.UpdateStudentGroupAsync(studentGroup, CancellationToken.None)).Returns(studentGroup);
        
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StudentGroupVM>();
        result!.Id.Should().Be(studentGroupId);
        A.CallTo(()=> _studentGroupRepository.GetStudentGroupByIdAsync(studentGroupId)).MustHaveHappened();
        A.CallTo(()=> _studentGroupRepository.UpdateStudentGroupAsync(A<StudentGroupEntity>.That.Matches(g=>
            g.Id == studentGroupId ), CancellationToken.None)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }
}
