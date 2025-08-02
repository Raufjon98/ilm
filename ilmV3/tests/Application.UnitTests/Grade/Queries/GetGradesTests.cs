using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Grade.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Grade.Queries;

public class GetGradesTests
{
    private readonly IMediator  _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetGradesTests()
    {
        var services = new ServiceCollection();
        var options  = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_=> _context);
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(GetGradesQueryHandler).Assembly));
        if (!_context.Grades.Any())
        {
            for (var i = 1; i < 10; i++)
            {
                _context.Grades.Add(
                    new GradeEntity
                    {
                        Id = i,
                        StudentId = i,
                        ClassDay = "getTest" + i,
                        Date = new DateOnly(i, i, i),
                        SubjectId = i,
                        TeacherId = i,
                        Grade = i
                    });
            }
            _context.SaveChanges();
        }
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnAllGrades()
    {
        //Arrange
        var query = new GetGradesQuery();
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        IEnumerable<GradeVM> gradeVms = result.ToList();
        gradeVms.Should().NotBeNull();
        gradeVms.Should().BeOfType<List<GradeVM>>();

    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
