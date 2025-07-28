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

public class GetTeacherTests
{
    private readonly IMediator  _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetTeacherTests()
    {
        var services = new ServiceCollection();
        var options= new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        services.AddScoped<IAplicationDbContext>(_=> _context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetTeacherQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnTeacher()
    {
        //Arrange 
        var teacherId = 1;
        TeacherEntity teacher = new TeacherEntity{Id = teacherId, Name = "Nicolas Jackson"};
        await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync();
        var query = new GetTeacherQuery(teacherId);
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TeacherVM>();
        result.Id.Should().Be(teacherId);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _context.Dispose();
        _provider.Dispose();
    }
}
