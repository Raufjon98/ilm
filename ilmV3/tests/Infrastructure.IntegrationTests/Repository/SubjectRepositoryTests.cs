using FluentAssertions;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Data;
using ilmV3.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.IntegrationTests.Repository;

public class SubjectRepositoryTests
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly ApplicationDbContext _context;

    public SubjectRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("SubjectRepositoryTest")
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        for (int i = 1; i < 5; i++)
        {
            _context.Subjects.Add(
                new SubjectEntity { Id = i, Name = $"Subject {i}", TeacherId = i });
        }
        _context.SaveChanges();
        _subjectRepository = new SubjectRepository(_context);
    }

    [Test]
    public async Task ShouldCreateAndRetrieveSubject()
    {
        //Arrange
        SubjectEntity subject = new SubjectEntity { Name = "Asp.Net core", TeacherId = 1 };
        
        //Act
        var result = await _subjectRepository.CreateSubjectAsync(subject, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Asp.Net core");
    }

    [Test]
    public async Task ShouldReturnSubject()
    {
        //Arrange
        int subjectId = 1;
        
        //Act
        var result = await _subjectRepository.GetSubjectByIdAsync(subjectId);
        
        //Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(subjectId);
    }
    
    [Test]
    public async Task ShouldUpdateAndRetrieveSubject()
    {
        //Arrange
        var subject = await _subjectRepository.GetSubjectByIdAsync(2);
        subject!.Name = "Updated Subject";
        
        //Act
        var result =await _subjectRepository.UpdateSubjectAsync(subject!, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Updated Subject");
    }

    [Test]
    public async Task ShouldDeleteSubject()
    {
        //Arrange
        var subject = await _subjectRepository.GetSubjectByIdAsync(5);
        
        //Act
        var result = await _subjectRepository.DeleteSubjectAsync(subject!, default);
        
        //Assert
        result.Should().BeTrue();
    }
    
    [OneTimeTearDown]
    public  void OneTimeTearDown() { _context.Dispose(); }
}
