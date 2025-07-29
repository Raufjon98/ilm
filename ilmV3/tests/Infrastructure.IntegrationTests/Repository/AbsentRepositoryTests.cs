using FluentAssertions;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Data;
using ilmV3.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.IntegrationTests.Repository;

public class AbsentRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly IAbsentRepository _absentRepository;
    
    public AbsentRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("AbsentRepositoryTests")
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        for (int i = 1; i < 5; i++)
        {
            _context.Absents.Add(
                new AbsentEntity
                {
                    StudentId = i,
                    TeacherId = i,
                    SubjectId = i,
                    ClassDay = "day" + i,
                    Date = DateOnly.FromDateTime(DateTime.Today),
                    Absent = true
                });
        }
        _context.SaveChanges();
        _absentRepository = new AbsentRepository(_context);
    }

    [Test]
    public async Task ShouldCreateAndRetrieveAbsent()
    {
        //Arrange
        AbsentEntity absent = new AbsentEntity
        {
            Id = 12,
            StudentId = 1,
            TeacherId = 1,
            SubjectId = 1,
            ClassDay = "Day1",
            Date = DateOnly.MaxValue
        };
        
        //Act
        var result = await _absentRepository.CreateAbsentAsync(absent, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<AbsentEntity>();
        result.StudentId.Should().Be(absent.StudentId);
        result.TeacherId.Should().Be(absent.TeacherId);
    }
    
    [Test]
    public async Task ShouldReturnAbsentById()
    {
        //Arrange
        int absentId = 3;
        
        //Act
        var result = await _absentRepository.GetAbsentByIdAsync(absentId);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<AbsentEntity>();
        result!.Id.Should().Be(absentId);
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveAbsent()
    {
        //Arrange
        var absentFromDb = await _context.Absents.FirstOrDefaultAsync(a => a.Id == 4);
        absentFromDb!.StudentId = 17;
        
        //Act
        var result = await _absentRepository.UpdateAbsentAsync(absentFromDb, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<AbsentEntity>();
        result.StudentId.Should().Be(17);
    }

    [Test]
    public async Task ShouldDeleteAbsent()
    {
        //Arrange
        var absent = await _context.Absents.FirstOrDefaultAsync(a => a.Id == 1);
        
        //Act
        var result = await _absentRepository.DeleteAbsentAsync(absent!, default);
        
        //Assert
        result.Should().BeTrue();
    }

 
    
    [OneTimeTearDown]
    public void OneTimeTearDown() => _context.Dispose();
}
