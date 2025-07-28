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

public class StudentRegisterTests
{
    private readonly IMediator  _mediator;
    private readonly ServiceProvider _provider;
    private readonly ITokenService _tokenService;
    private readonly IStudentRepository _studentRepository;
    private readonly IIdentityService _identityService;
    private  const string role = "Student";

    public StudentRegisterTests()
    {
        var services = new ServiceCollection();
        _studentRepository = A.Fake<IStudentRepository>();
        _identityService = A.Fake<IIdentityService>();
        _tokenService = A.Fake<ITokenService>();
        services.AddSingleton(_studentRepository);
        services.AddSingleton(_identityService);
        services.AddSingleton(_tokenService);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(StudentRegisterCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldCreateAndRetrieveStudent()
    {
        //Arrange
        var externalUserId = 4;
        RegisterDto register = new RegisterDto{Email = "student@test.com", Password = "password", UserName = "Student"};
        var command = new StudentRegisterCommand(register);
        StudentEntity student = new StudentEntity{Name = register.UserName, Id = externalUserId};
        ApplicationUser user = new ApplicationUser{Email = register.Email, ExternalUserId= externalUserId, UserName = register.UserName}; 
        A.CallTo(() => _studentRepository.CreateStudentAsync(A<StudentEntity>._, CancellationToken.None)).Returns(student);
        A.CallTo(() => _identityService.CreateUserAsync(externalUserId, register, role)).Returns(user);
        A.CallTo(()=> _tokenService.CreateToken(A<ApplicationUserDto>.Ignored)).Returns("mocked-jwt-token");
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreatedUserDto>();
        result!.Token.Should().Be("mocked-jwt-token");
        A.CallTo(()=> _studentRepository.CreateStudentAsync(A<StudentEntity>.That.Matches(s=>s.Name == register.UserName), A<CancellationToken>._)).MustHaveHappened();
        A.CallTo(()=>_tokenService.CreateToken(A<ApplicationUserDto>.That.Matches(u=>
            u.UserName == register.UserName && 
            u.Role == role))).MustHaveHappened();
        A.CallTo(()=> _identityService.CreateUserAsync(externalUserId, register, role)).MustHaveHappened();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() { _provider.Dispose(); }
}
