using FluentAssertions;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Data;
using ilmV3.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.IntegrationTests.Repository;

public class TeacherRepositoryTests
{
    private readonly ApplicationDbContext  _context;
    private readonly ITeacherRepository _teacherRepository;

    public TeacherRepositoryTests()
    {
        var options  = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TeacherRepositoryTest")
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        for (int i = 1; i < 5; i++)
        {
            _context.Teachers.Add(
                new TeacherEntity { Id = i, Name = "Teacher" + i, });
        }
        _context.SaveChanges();
        _teacherRepository = new TeacherRepository(_context);
    }

    [Test]
    public async Task ShouldReturnTeacherById()
    {
        //Arrange
        int teacherId = 1;
        
        //Act
        var result = await _teacherRepository.GetTeacherByIdAsync(teacherId);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TeacherEntity>();
        result!.Id.Should().Be(teacherId);
    }

    [Test]
    public async Task ShouldCreateAndRetrieveTeacher()
    {
        //Arrange
        TeacherEntity? teacher = new TeacherEntity { Name = "CreatedTeacher" };
        
        //Act
        var result = await _teacherRepository.CreateTeacherAsync(teacher, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TeacherEntity>();
        result.Name.Should().Be(teacher.Name);
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveTeacher()
    {
        //Arrange
        var teacher = await _teacherRepository.GetTeacherByIdAsync(2);
        teacher!.Name = "UpdatedTeacher";
        
        //Act
        var result = await _teacherRepository.UpdateTeacherAsync(teacher, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TeacherEntity>();
        result.Name.Should().Be(teacher.Name);
    }

    [Test]
    public async Task ShouldDeleteTeacher()
    {
        //Arrange
        var teacher = await _teacherRepository.GetTeacherByIdAsync(5);
        
        //Act
        var result = await _teacherRepository.DeleteTeacherAsync(teacher!, default);
        
        //Assert
        result.Should().BeTrue();
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown(){_context.Dispose();}
    }
