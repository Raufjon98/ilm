using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Teacher.Commands.DeleteTeacher;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Teacher.Commands;

public class DeleteTeacherTests
{
    private readonly IMediator  _mediator;
    private readonly ITeacherRepository  _teacherRepository;
    private readonly ServiceProvider _provider;

    public DeleteTeacherTests()
    {
        var services = new ServiceCollection();
        _teacherRepository = A.Fake<ITeacherRepository>();
        services.AddSingleton(_teacherRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(DeleteTeacherCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldDeleteTeacher()
    {
        //Arrange
        var teacherId = 1;
        var command = new DeleteTeacherCommand(teacherId);
        TeacherEntity teacher = new TeacherEntity {Id = teacherId, Name = "DeleteTeacher"};
        A.CallTo(()=> _teacherRepository.GetTeacherByIdAsync(teacher.Id)).Returns(teacher);
        A.CallTo(() => _teacherRepository.DeleteTeacherAsync(teacher, CancellationToken.None)).Returns(true);
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().BeTrue();
        A.CallTo(() => _teacherRepository.DeleteTeacherAsync(A<TeacherEntity>.That.Matches(t=>
            t.Id == teacherId), A<CancellationToken>._)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }
}
