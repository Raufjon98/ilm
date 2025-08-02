using FluentAssertions;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Data;
using ilmV3.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.IntegrationTests.Repository;

public class StudentRepositoryTests
{
    private readonly IStudentRepository _studentRepository;
    private readonly ApplicationDbContext _context;

    public StudentRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase("StudentRepositoryTest")
        .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        for (int i = 1; i < 5; i++)
        {
            _context.Students.Add(
                new StudentEntity { Id = i, Name = "Student " + i });
        }
        _context.SaveChanges();
        _studentRepository = new StudentRepository(_context);
    }

    [Test]
    public async Task ShouldCreateAndRetrieveStudent()
    {
        //Arrange
        StudentEntity student = new StudentEntity { Name = "Jackie" };
        
        //Act
        var result = await _studentRepository.CreateStudentAsync(student, default);
        
        //Assert
        result.Should().NotBeNull();
        student.Name.Should().Be("Jackie");
    }

    [Test]
    public async Task ShouldReturnStudent()
    {
        //Arrange
        int studentId = 1;
        
        //Act
        var result = await _studentRepository.GetStudentByIdAsync(studentId);
        
        //Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(studentId);
    }
    
    [Test]
    public async Task ShouldUpdateAndRetrieveStudent()
    {
        //Arrange
        var student = await _studentRepository.GetStudentByIdAsync(2);
        student!.Name = "Sahil";
        
        //Act
        var result = await _studentRepository.UpdateStudentAsync(student, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Sahil");
    }

    [Test]
    public async Task ShouldDeleteStudent()
    {
        //Arrange
        var student = await _studentRepository.GetStudentByIdAsync(5);
        
        //Act
        var result = await _studentRepository.DeleteStudentAsync(student!, default);
        
        //Assert
        result.Should().BeTrue();
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown() {_context.Dispose();}
}
