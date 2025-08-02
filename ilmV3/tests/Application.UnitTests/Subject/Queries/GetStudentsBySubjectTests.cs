using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Student.Queries;
using ilmV3.Application.StudentGroup.Queries;
using ilmV3.Application.Subject.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Subject.Queries;

public class GetStudentsBySubjectTests
{
    private readonly IMediator  _mediator;
    private readonly ServiceProvider _provider;
    private readonly ApplicationDbContext _context;

    public GetStudentsBySubjectTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_ => _context);
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(GetStudentsBySubjectIdQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnStudentsBySubject()
    {
        //Arrange
        var subjectId = 1;
        var query = new GetStudentsBySubjectIdQuery(subjectId);
        _context.StudentGroups.Add(
            new StudentGroupEntity { 
                Name = "Testers", 
                SubjectId = subjectId, 
                Students = new List<StudentEntity>()
                {
                    new StudentEntity{Name = "Mufaso"},
                    new StudentEntity{Name = "Jackie"},
                    new StudentEntity(){Name = "Qurbon"}
                }
            });
        await _context.SaveChangesAsync();
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        IEnumerable<StudentVM> studentVms = result.ToList();
        studentVms.Should().NotBeNull();
        studentVms.Should().BeOfType<List<StudentVM>>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
