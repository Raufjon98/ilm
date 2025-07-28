using FluentAssertions;
using ilmV3.Application.Admin.Queries;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Admin.Queries;

public class GetAdminsTests
{
    private readonly IMediator  _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetAdminsTests()
    {
        var services = new ServiceCollection();
        var options  = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();

        if (!_context.Admins.Any())
        {
            for (int i = 1; i < 10; i++)
            {
                _context.Admins.Add(new AdminEntity { Id = i, Name = "admin " + i, });
            }
            _context.SaveChanges();
        }
        services.AddScoped<IAplicationDbContext>(_=>_context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetAdminsQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnAllAdmins()
    {
        //Arrange
        var query = new GetAdminsQuery();
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<AdminVM>>();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
        _context?.Dispose();
    }
}
