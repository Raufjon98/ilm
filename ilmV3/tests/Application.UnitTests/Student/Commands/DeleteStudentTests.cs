using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Student.Commands.DeleteStudent;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Student.Commands;

public class DeleteStudentTests
{
    private readonly IMediator  _mediator;
    private readonly IStudentRepository  _studentRepository;
    private readonly ServiceProvider _provider;
    
    public DeleteStudentTests()
    {
        var services = new ServiceCollection();
        _studentRepository = A.Fake<IStudentRepository>();
        services.AddSingleton(_studentRepository);
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(DeleteStudentCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldDeleteStudent()
    {
        //Arrange
        var studentId = 9;
        StudentEntity student = new StudentEntity
        {
            Id = studentId,
            Name = "DeleteTestStudent"
        };
        A.CallTo(()=> _studentRepository.GetStudentByIdAsync(studentId)).Returns(student);
        A.CallTo(() =>_studentRepository.DeleteStudentAsync(student, CancellationToken.None)).Returns(true);
        var command = new DeleteStudentCommand(studentId);
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().BeTrue();
        A.CallTo(()=> _studentRepository.GetStudentByIdAsync(studentId)).MustHaveHappened();
        A.CallTo(()=> _studentRepository.DeleteStudentAsync(A<StudentEntity>.That.Matches(s=>
            s.Id == studentId &&
            s.Name == student.Name), CancellationToken.None)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }
}
