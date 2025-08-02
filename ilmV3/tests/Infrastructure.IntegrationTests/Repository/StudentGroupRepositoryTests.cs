using FluentAssertions;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Data;
using ilmV3.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.IntegrationTests.Repository;

public class StudentGroupRepositoryTests
{
    private readonly IStudentGroupRepository _studentGroupRepository;
    private readonly ApplicationDbContext _context;

    public StudentGroupRepositoryTests()
    {
        var options  = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("StudentGroupRepositoryTests")
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        for (int i = 1; i < 5; i++)
        {
            _context.StudentGroups.Add(
                new StudentGroupEntity { Name = "group" + i, CodeName = i + "55-20-20", SubjectId = i, });
        }
        _context.SaveChanges();
        _studentGroupRepository = new StudentGroupRepository(_context);
    }

    [Test]
    public async Task CreateAndRetrieveStudentGroup()
    {
        //Arrange
        StudentGroupEntity studentGroup = new StudentGroupEntity
        {
            Name = "English as a second language", CodeName = "1-25-25-20", SubjectId = 1,
        };
        
        //Act
        var result = await _studentGroupRepository.CreateStudentGroupAsync(studentGroup, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(studentGroup.Name);
        result.CodeName.Should().Be(studentGroup.CodeName);
    }

    [Test]
    public async Task ShouldReturnStudentGroup()
    {
        //Arrange
        int studentGroupId = 1;
        
        //Act
        var result = await _studentGroupRepository.GetStudentGroupByIdAsync(studentGroupId);
        
        //Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(studentGroupId);
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveStudentGroup()
    {
        //Arrange
        var studentGroup = await _studentGroupRepository.GetStudentGroupByIdAsync(1);
        studentGroup!.Name = "Updated Group"; 
        
        //Act
        var result = await _studentGroupRepository.UpdateStudentGroupAsync(studentGroup!, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Updated Group");
    }

    [Test]
    public async Task ShouldDeleteStudentGroup()
    {
        //Arrange
        var studentGroup = await _studentGroupRepository.GetStudentGroupByIdAsync(5);
        
        //Act
        var result = await _studentGroupRepository.DeleteStudentGroupAsync(studentGroup!, default);
        
        //Assert
        result.Should().BeTrue();
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown(){_context.Dispose();}
}
