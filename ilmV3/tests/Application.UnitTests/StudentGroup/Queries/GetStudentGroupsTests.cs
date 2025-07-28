using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.StudentGroup.Queries;

public class GetStudentGroupsTests
{
    private readonly IMediator  _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetStudentGroupsTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        services.AddScoped<IAplicationDbContext>(_=>_context);
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(GetStudentGroupsQuery).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnStudentGroups()
    {
        //Arrange
        _context.StudentGroups.AddRange(new List<StudentGroupEntity>()
        {
            new StudentGroupEntity { Id = 1, Name = "English For beginners", },
            new  StudentGroupEntity { Id = 2, Name = "French For beginners", },
            new StudentGroupEntity {Id = 3, Name = "Web developer avi .Net"}
        });
        await _context.SaveChangesAsync();
        var query = new GetStudentGroupsQuery();
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        IEnumerable<StudentGroupVM> studentGroupVms = result.ToList();
        studentGroupVms.Should().NotBeNull();
        studentGroupVms.Should().BeOfType<List<StudentGroupVM>>();
        
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _context.Dispose();
        _provider.Dispose();
    }
}
