using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Account.Commands.Register;
using ilmV3.Application.Admin.Commands.DeleteAdmin;
using ilmV3.Application.Admin.Queries;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Data;
using ilmV3.Infrastructure.Identity;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Admin.Commands;

public class DeleteAdminTests
{
    private readonly IMediator  _mediator;
    private readonly IIdentityService _identityService;
    private readonly IAdminRepository _adminRepository;
    private readonly ServiceProvider _provider;

    public DeleteAdminTests()
    {
        var services = new ServiceCollection();
        _adminRepository = A.Fake<IAdminRepository>();
        _identityService = A.Fake<IIdentityService>();
        services.AddSingleton(_identityService);
        services.AddSingleton(_adminRepository);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(DeleteAdminCommandHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldDeleteAdmin()
    {
        //Arrange
        var userId = "my-admin";
        var adminId = 11;
        ApplicationUser user = new ApplicationUser
        {
            Id = userId,
            ExternalUserId = adminId
        };
        AdminEntity admin = new AdminEntity { Id = adminId };
        A.CallTo(() => _adminRepository.GetAdminByIdAsync(adminId)).Returns(admin);
        A.CallTo(() => _identityService.GetUserByIdAsync(userId)).Returns(user);
        A.CallTo(()=> _adminRepository.DeleteAdminAsync(admin, default)).Returns(true);
        A.CallTo(()=> _identityService.DeleteUserAsync(userId)).Returns(Task.FromResult(Result.Success())); 
        
        var command = new DeleteAdminCommand(userId);
        
        //Act
        var result = await _mediator.Send(command);
        
        //Assert
        
        result.Should().BeTrue();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider?.Dispose();
    }
}
