using FluentAssertions;
using ilmV3.Domain.Entities;
using ilmV3.Infrastructure.Data;
using ilmV3.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.IntegrationTests.Repository;

public class AdminRepositoryTests
{
    private readonly ApplicationDbContext _context;
    private readonly AdminRepository _adminRepository;

    public AdminRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("AdminRepositoryTests")
            .Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        for (int i = 1; i < 5; i++)
        {
            _context.Admins.Add(
                new AdminEntity { Name = "Admin " + i });
        }
        _context.SaveChanges();
        _adminRepository = new AdminRepository(_context);
    }

    [Test]
    public async Task ShouldCreateAndRetrieveAdmin()
    {
        //Arrange
        AdminEntity admin = new AdminEntity { Name = "Administrator" };
        
        //Act
        var result = await _adminRepository.CreateAdminAsync(admin, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Administrator");
    }

    [Test]
    public async Task ShouldReturnAdminById()
    {
        //Arrange
        int adminId = 1;
        
        //Act
        var result = await _adminRepository.GetAdminByIdAsync(adminId);
        
        result.Should().NotBeNull();
        result!.Id.Should().Be(adminId);
    }

    [Test]
    public async Task ShouldUpdateAndRetrieveAdmin()
    {
        //Arrabge
        var admin = await _adminRepository.GetAdminByIdAsync(1);
        admin!.Name = "Mufaso";
        
        //Act
        var result = await _adminRepository.UpdateAdminAsync(admin, default);
        
        //Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Mufaso");
    }

    [Test]
    public async Task ShouldDeleteAdmin()
    {
        //Arrange
        var admin = await _adminRepository.GetAdminByIdAsync(5);
        
        //Act
        var result = await _adminRepository.DeleteAdminAsync(admin!, default);
        
        //Assert
        result.Should().BeTrue();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() { _context.Dispose(); }
}
