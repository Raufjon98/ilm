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

public class GetGradeTests
{
  private readonly IMediator  _mediator;
  private readonly ApplicationDbContext _context;
  private readonly ServiceProvider _provider;

  public GetGradeTests()
  {
      var services = new ServiceCollection();
      var options = new DbContextOptionsBuilder<ApplicationDbContext>()
          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
          .Options;
      _context = new ApplicationDbContext(options);
      services.AddScoped<IAplicationDbContext>(_=> _context);
      services.AddMediatR(cfg=> cfg.RegisterServicesFromAssembly(typeof(GetGradeQueryHandler).Assembly));
      _provider = services.BuildServiceProvider();
      _mediator = _provider.GetRequiredService<IMediator>();
  }

  [Test]
  public async Task ShouldReturnGrade()
  {
      //Arrange
      var gradeId = 1;
      GradeEntity grade = new GradeEntity
      {
          Id = gradeId,
          StudentId = 4,
          TeacherId = 4,
          Grade = 6,
          SubjectId = 6,
          Date = DateOnly.MinValue,
          ClassDay = "getTest"
      };
          _context.Grades.Add(grade);
          await _context.SaveChangesAsync(CancellationToken.None);
          var query= new GetGradeQuery(gradeId);
      
      //Act
      var  result = await _mediator.Send(query);
      
      //Assert
      result.Should().NotBeNull();
      result.Id.Should().Be(gradeId);
      result.StudentId.Should().Be(grade.StudentId);
  }

  [OneTimeTearDown]
  public void TearDown()
  {
      _provider.Dispose();
      _context.Dispose();
  }
}
