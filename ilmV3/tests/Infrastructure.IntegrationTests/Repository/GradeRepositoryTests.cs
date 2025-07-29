using FluentAssertions;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Data;
using ilmV3.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.IntegrationTests.Repository;

public class GradeRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly IGradeRepository _gradeRepository;

    public GradeRepositoryTests()
    {
        var options  = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("GradeRepositoryTest")
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        for (int i = 1; i < 5; i++)
        {
            _context.Grades.Add(
                new GradeEntity
                {
                    Id = i,
                    StudentId = i,
                    TeacherId = i,
                    SubjectId = i,
                    ClassDay = "day" + i,
                    Date = DateOnly.FromDateTime(DateTime.Today)
                });
        }
        _context.SaveChanges();
        _gradeRepository = new GradeRepository(_context);
    }

    [Test]
    public async Task ShouldCreateAndRetrieveGrade()
    {
        //Arrange
        GradeEntity grade = new GradeEntity
        {
            StudentId = 15,
            TeacherId = 15,
            SubjectId = 15,
            ClassDay = "day" + 15,
            Date = DateOnly.FromDateTime(DateTime.Today)
        };
        
        //Act
        var result = await _gradeRepository.CreateGradeAsync(grade, default);
        
        //Assert
        result.Should().NotBeNull();
        result.StudentId.Should().Be(grade.StudentId);
        result.TeacherId.Should().Be(grade.TeacherId);
        result.SubjectId.Should().Be(grade.SubjectId);
    }

    [Test]
    public async Task ShouldReturnGradeById()
    {
        //Arrange
        int gradeId = 2;
        
        //Act
        var result = await _gradeRepository.GetGradeByIdAsync(gradeId);
        
        //
        result.Should().NotBeNull();
        result!.Id.Should().Be(gradeId);
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveGrade()
    {
        //Arrange
        var grade = await _gradeRepository.GetGradeByIdAsync(2);
        grade!.StudentId = 3;
        
        //Act
        var result = await _gradeRepository.UpdateGradeAsync(grade, default);
        
        //Assert
        result.Should().NotBeNull();
        result.StudentId.Should().Be(grade.StudentId);
    }

    [Test]
    public async Task ShouldDeleteGrade()
    {
        //Arrange
        var grade = await _gradeRepository.GetGradeByIdAsync(1);
        
        //Act
        var result = await _gradeRepository.DeleteGradeAsync(grade!, default);
        
        //Assert
        result.Should().BeTrue();
    }
    
    
    [OneTimeTearDown]
    public void OneTimeTearDown(){ _context.Dispose();}
}
