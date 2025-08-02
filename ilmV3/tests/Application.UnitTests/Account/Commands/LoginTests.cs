using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Account.Commands.Login;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.Constants;
using ilmV3.Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Account.Commands;

public class LoginTests
{
    private readonly IMediator  _mediator;
    private readonly ITokenService _tokenService;
    private readonly IIdentityService _identityService;
    private readonly ServiceProvider _provider;

    public LoginTests()
    {
        var services = new ServiceCollection();
        _tokenService = A.Fake<ITokenService>();
        _identityService = A.Fake<IIdentityService>();
        services.AddSingleton(_tokenService);
        services.AddSingleton(_identityService);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldLoginReturnCreatedUser()
    {
        //Arrange
        
       var roles  =new List<Roles>();
       var rolesStr = roles.Select(r => r.ToString()!).ToList();
           //.Select(r => r.ToString()).ToList();
        LoginDto login = new LoginDto{Email = "user@test.com", Password = "password"};
        var command = new LoginCommand(login);
        ApplicationUser user = new ApplicationUser{Email = login.Email, UserName = login.Email};
        A.CallTo(() => _identityService.GetUserByUsernameAsync(login.Email)).Returns(user);
        A.CallTo(()=> _identityService.GetUserRolesAsync(user)).Returns(rolesStr);
        A.CallTo(()=> _identityService.CheckPasswordAsync(A<ApplicationUser>.Ignored, login.Password)).Returns(true);
        A.CallTo(() => _tokenService.CreateToken(A<ApplicationUserDto>.Ignored)).Returns("My-fake-token");
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreatedUserDto>();
        result!.Email.Should().Be(user.Email);
        result.UserName.Should().Be(user.UserName);
        A.CallTo(()=> _identityService.CheckPasswordAsync(A<ApplicationUser>._, login.Password)).MustHaveHappened();
        A.CallTo(()=> _identityService.GetUserByUsernameAsync(login.Email)).MustHaveHappened();
        A.CallTo(() => _identityService.GetUserRolesAsync(user)).MustHaveHappened();
        A.CallTo(()=> _tokenService.CreateToken(A<ApplicationUserDto>._)).MustHaveHappenedOnceExactly();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() { _provider.Dispose(); }
}
