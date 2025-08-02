using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Teacher.Commands.UpdateTeacher;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Identity;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Teacher.Commands;

public class UpdateTeacherTests
{
    private readonly IMediator _mediator;
    private readonly ITeacherRepository  _teacherRepository;
    private readonly ServiceProvider _provider;
    private readonly IIdentityService  _identityService;

    public UpdateTeacherTests()
    {
        var services = new ServiceCollection();
        _teacherRepository = A.Fake<ITeacherRepository>();
        _identityService = A.Fake<IIdentityService>();
        services.AddSingleton(_identityService);
        services.AddSingleton(_teacherRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(UpdateTeacherCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldUpdateTeacher()
    {
        //Arrange
        var userId = "TeachersUserId";
        var teacherId = 1;
        TeacherDto teacherDto = new TeacherDto{ Name = "Erzhan" };
        TeacherEntity teacher = new TeacherEntity{ Id = teacherId, Name = "Erzhan"};
        ApplicationUser user = new ApplicationUser { UserName = "Erzhan",  Id = userId, Email = "some@example", ExternalUserId = teacherId};
        var command = new UpdateTeacherCommand(userId, teacherDto);
        A.CallTo(()=> _teacherRepository.GetTeacherByIdAsync(teacherId)).Returns(teacher);
        A.CallTo(()=> _identityService.GetUserByIdAsync(userId)).Returns(user);
        A.CallTo(()=> _teacherRepository.UpdateTeacherAsync(A<TeacherEntity>._, A<CancellationToken>._)).Returns(teacher);
        A.CallTo(()=> _identityService.UpdateUserAsync(A<ApplicationUser>._)).Returns(user);
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TeacherVM>();
        result!.Id.Should().Be(teacherId);
        A.CallTo(()=> _teacherRepository.GetTeacherByIdAsync(teacherId)).MustHaveHappened();
        A.CallTo(()=> _teacherRepository.UpdateTeacherAsync(A<TeacherEntity>.That.Matches(t=>
            t.Id == teacherId &&
            t.Name == teacher.Name), A<CancellationToken>._)).MustHaveHappenedOnceExactly();
        A.CallTo(()=>_identityService.GetUserByIdAsync(userId)).MustHaveHappened();
        A.CallTo(() => _identityService.UpdateUserAsync(A<ApplicationUser>.That.Matches(u=>
            u.Id == userId &&
            u.ExternalUserId == teacherId))).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
    }
    
}
