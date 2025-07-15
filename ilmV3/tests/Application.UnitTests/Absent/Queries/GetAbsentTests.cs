using System.Diagnostics.Contracts;
using FluentAssertions;
using ilmV3.Application.Absent.Queries.GetAbsent;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Absent.Queries;
public class GetAbsentTests
{
    private readonly ApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly ServiceProvider _provider;

    public GetAbsentTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetAbsentByIdQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldGetAbsent()
    {
        //Arrange
        var absentId =1;
        AbsentEntity absent = new AbsentEntity
        {
            Id = absentId,
            StudentId = 1,
            TeacherId = 1,
            SubjectId = 1,
            ClassDay = "getDay",
            Date = DateOnly.FromDayNumber(1),
        };
        _context.Absents.Add(absent);
        await _context.SaveChangesAsync(default);

        var query = new GetAbsentByIdQuery(absentId);

        //Act
        var result = await _mediator.Send(query);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<AbsentVM>();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider?.Dispose();
        _context?.Dispose();
    }
}
