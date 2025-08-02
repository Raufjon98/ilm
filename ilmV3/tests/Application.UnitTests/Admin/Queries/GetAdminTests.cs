using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using ilmV3.Application.Admin.Queries;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NUnit.Framework;


namespace ilmV3.Application.UnitTests.Admin.Queries;

public class GetAdminTests
{
    private readonly IMediator  _mediator;
    private readonly ServiceProvider _provider;
    private readonly ApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    
    public GetAdminTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _identityService = A.Fake<IIdentityService>();
        services.AddSingleton(_identityService);
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetAdminQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnAdmin()
    {
        //Arrange 
        var userId = "test-user-id";
        var externalId = 5;
       var query  = new GetAdminQuery(userId);
       ApplicationUser user = new ApplicationUser
       {
           Id = userId,
           UserName = "Alsotest",
           ExternalUserId = externalId
       };
       AdminEntity admin = new AdminEntity { Id = externalId, Name = user.UserName };
       _context.Users.Add(user);
       _context.Admins.Add(admin);
       await _context.SaveChangesAsync();
       A.CallTo(() => _identityService.GetUserByIdAsync(userId)).Returns(user);
       
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<AdminVM>();
        result.Name.Should().Be(admin.Name);
        result.Id.Should().Be(admin.Id);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider?.Dispose();
        _context?.Dispose();
    }
}
