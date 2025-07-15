using FluentAssertions;
using ilmV3.Application.Absent.Queries.GetAbsent;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Absent.Queries;
public class GetAbsentsTests
{
    private readonly IMediator _mediator;
    private readonly ServiceProvider _provider;
    private readonly ApplicationDbContext _context;

    public GetAbsentsTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAbsentsQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnAbsentVMList()
    {
        //Arrange
        var query = new GetAbsentsQuery();

        //Act
        var result = await _mediator.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<AbsentVM>>();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider?.Dispose();
        _context?.Dispose();
    }
}
