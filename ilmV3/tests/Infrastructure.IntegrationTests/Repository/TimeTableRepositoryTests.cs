using FluentAssertions;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Data;
using ilmV3.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.IntegrationTests.Repository;

public class TimeTableRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly ITimeTableRepository _timeTableRepository;

    public TimeTableRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("TimeTableRepositoryTest")
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        for (int i = 1; i < 5; i++)
        {
            _context.TimeTables.Add(
                new TimeTableEntity
                {
                    Id = i,
                    Name = $"TimeTable {i}",
                    TeacherId = i,
                    Audience = $"{i + i}",
                    Date = DateOnly.MaxValue,
                    StudentGroupId = i,
                    SubjectId = i,
                });
        }
        _context.SaveChanges();
        _timeTableRepository = new TimeTableRepository(_context);
    }

    [Test]
    public async Task ShouldReturnTimeTableById()
    {
        //Arrange
        int timeTableId = 1;
        
        //Act
        var result = await _timeTableRepository.GetTimeTableByIdAsync(timeTableId);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TimeTableEntity>();
        result!.Id.Should().Be(timeTableId);
    }

    [Test]
    public async Task ShouldCreateAndRetrieveTimeTable()
    {
        //Arrange
        TimeTableEntity timeTable = new TimeTableEntity
        {
            Name = "CreateTimeTable",
            TeacherId = 1,
            Date = DateOnly.MaxValue,
            StudentGroupId = 1,
            SubjectId = 1,
            Audience = "206",
        };
        
        //Act
        var result = await _timeTableRepository.CreateTimeTableAsync(timeTable, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TimeTableEntity>();
        result.Name.Should().Be(timeTable.Name);
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveTimeTable()
    {
        //Arrange
        var timeTable = await _timeTableRepository.GetTimeTableByIdAsync(2);
        timeTable!.Name = "UpdateTimeTable";
        
        //Act
        var result = await _timeTableRepository.UpdateTimeTableAsync(timeTable, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<TimeTableEntity>();
        result.Name.Should().Be("UpdateTimeTable");
    }

    [Test]
    public async Task ShouldDeleteTimeTable()
    {
        //Arrange
        var timeTable = await _timeTableRepository.GetTimeTableByIdAsync(5);
        
        //Act
        var result = await _timeTableRepository.DeleteTimeTableAsync(timeTable!, default);
        
        //Assert
        result.Should().BeTrue();
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDown() { _context.Dispose(); }

}
