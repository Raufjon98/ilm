using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Teacher.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Teacher.Queries;

public class GetTeachersTests
{
    private readonly ApplicationDbContext _context;
    private readonly IMediator  _mediator;
    private readonly ServiceProvider _provider;

    public GetTeachersTests()
    {
        var services = new ServiceCollection();
        var options=  new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_=>_context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetTeachersQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnAllTeachers()
    {
        //Arrange
        _context.Teachers.AddRange(new List<TeacherEntity>()
        {
            new TeacherEntity{Name = "Mufaso"},
            new TeacherEntity{Name = "Jackson"},
            new TeacherEntity{Name = "Nicolas"}
        });
        await _context.SaveChangesAsync();
        var query = new GetTeachersQuery();
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        IEnumerable<TeacherVM> teacherVms = result.ToList();
        teacherVms.Should().NotBeNull();
        teacherVms.Should().BeOfType<List<TeacherVM>>();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
