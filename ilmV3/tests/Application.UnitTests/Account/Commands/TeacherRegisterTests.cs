using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Account.Commands.Register;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Identity;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Account.Commands;

public class TeacherRegisterTests
{
    private readonly IMediator  _mediator;
    private readonly ServiceProvider _provider;
    private readonly IIdentityService _identityService;
    private readonly ITeacherRepository _teacherRepository;
    private readonly ITokenService _tokenService;
    private const string role = "Teacher";

    public TeacherRegisterTests()
    {
        var services = new ServiceCollection();
        _teacherRepository  = A.Fake<ITeacherRepository>();
        _identityService = A.Fake<IIdentityService>();
        _tokenService = A.Fake<ITokenService>();
        services.AddSingleton(_teacherRepository);
        services.AddSingleton(_identityService);
        services.AddSingleton(_tokenService);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(TeacherRegisterCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldCreateAndRetrieveTeacher()
    {
        //Arrange 
        var teacherId = 1;
        var userId = "TheUserId";
        RegisterDto register = new RegisterDto
        {
            UserName = "NewTeacherTest",
            Email = "teacher@test.com",
            Password = "teacher@password" 
        };
        var command = new TeacherRegisterCommand(register);
        TeacherEntity teacher = new TeacherEntity { Name = register.UserName, Id = teacherId };
        ApplicationUser user = new ApplicationUser { UserName = register.UserName, Email = register.Email,  Id = userId, ExternalUserId = teacher.Id };
        A.CallTo(()=> _teacherRepository.CreateTeacherAsync(A<TeacherEntity>.Ignored, CancellationToken.None)).Returns(teacher);
        A.CallTo(()=> _identityService.CreateUserAsync(teacherId, register, role)).Returns(user);
        A.CallTo(()=> _tokenService.CreateToken(A<ApplicationUserDto>.Ignored)).Returns("my-new-teachers-token");
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreatedUserDto>();
        result!.Token.Should().Be("my-new-teachers-token");
        A.CallTo(()=> _teacherRepository.CreateTeacherAsync(A<TeacherEntity>.That.Matches(t=>
            t.Name == register.UserName), CancellationToken.None)).MustHaveHappenedOnceExactly();
        A.CallTo(()=> _identityService.CreateUserAsync(teacherId, register, role)).MustHaveHappenedOnceExactly();
        A.CallTo(()=>_tokenService.CreateToken(A<ApplicationUserDto>.Ignored)).MustHaveHappenedOnceExactly();        
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
    }
}
