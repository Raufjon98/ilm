using FluentAssertions;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Student.Queries;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ilmV3.Application.UnitTests.Student.Queries;

public class GetExcellentStudentsTests
{
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _context;
    private readonly ServiceProvider _provider;

    public GetExcellentStudentsTests()
    {
        var services = new ServiceCollection();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        services.AddScoped<IAplicationDbContext>(_=> _context);
        services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(GetExcellentStudentsQueryHandler).Assembly));
        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Test]
    public async Task ShouldReturnExcellentStudents()
    {
        //Arrange
        var studentId = 22;
        var query = new GetExcellentStudentsQuery();
        StudentEntity student = new StudentEntity { Id = studentId , Name = "Mufaso" };
        GradeEntity grade = new GradeEntity
        {
            StudentId = studentId,
            Grade = 10,
            SubjectId = 1,
            TeacherId = 5,
            Date = DateOnly.MaxValue,
            ClassDay = "day5"
        };
        _context.Students.Add(student);
        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();
        
        //Act
        var result = await _mediator.Send(query);
        
        //Assert
        IEnumerable<StudentWithGradeVM> studentWithGradeVms = result.ToList();
        studentWithGradeVms.Should().NotBeNull();
        studentWithGradeVms.Should().BeOfType<List<StudentWithGradeVM>>();
        studentWithGradeVms.Should().Contain(s=>s.Grade>=8);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _provider.Dispose();
        _context.Dispose();
    }
}
