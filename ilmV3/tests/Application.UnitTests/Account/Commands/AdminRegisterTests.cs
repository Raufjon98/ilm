using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Account.Commands.Login;
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

public class AdminRegisterTests
{
    private readonly IMediator  _mediator;
    private readonly IIdentityService _identityService;
    private readonly IAdminRepository _adminRepository;
    private readonly ITokenService _tokenService;
    private readonly ServiceProvider _provider;
    private const string role = "Administrator";

    public AdminRegisterTests()
    {
        var services = new ServiceCollection();
        _identityService = A.Fake<IIdentityService>();
        _adminRepository = A.Fake<IAdminRepository>();
        _tokenService = A.Fake<ITokenService>();
        services.AddSingleton(_adminRepository);
        services.AddSingleton(_tokenService);
        services.AddSingleton(_identityService);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(AdminRegisterCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldCreateAndRetrieveAdmin()
    {
        //Arrange
        var externalUserId = 5;
        var userId = "thiswillbecreatedUserId";
        RegisterDto register = new RegisterDto {UserName = "AdminForTest", Password = "admin@test1234!", Email = "admin@test"};
        AdminEntity admin = new AdminEntity { Id = externalUserId, Name = register.UserName };
        ApplicationUser user = new ApplicationUser { Id = userId, UserName = register.UserName, Email = register.Email , ExternalUserId = externalUserId };
        A.CallTo(()=> _adminRepository.CreateAdminAsync(admin, CancellationToken.None)).Returns(admin);
        A.CallTo(()=> _identityService.CreateUserAsync(externalUserId, register, role)).Returns(user);
        A.CallTo(() => _tokenService.CreateToken(A<ApplicationUserDto>.Ignored)).Returns("mocked-jwt-token");
        var command = new AdminRegisterCommand(register);
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreatedUserDto>();
        result!.Email.Should().Be(user.Email);
        result!.UserName.Should().Be(user.UserName);
        A.CallTo(()=> _adminRepository.CreateAdminAsync(A<AdminEntity>._, A<CancellationToken>._)).MustHaveHappened();
        A.CallTo(()=> _identityService.CreateUserAsync(A<int>.Ignored, A<RegisterDto>._, role)).MustHaveHappened();
        A.CallTo(() => _tokenService.CreateToken(A<ApplicationUserDto>.That.Matches(u=>u.UserName == register.UserName))).MustHaveHappened();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
    }
}
