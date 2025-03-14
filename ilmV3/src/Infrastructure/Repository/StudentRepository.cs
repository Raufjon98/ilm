using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Repository;
public class StudentRepository : IStudentRepository
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public StudentRepository(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<StudentEntity> CreateStudentAsync(StudentEntity student, CancellationToken cancellationToken)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync(cancellationToken);
        return student;
    }

    public async Task<bool> DeleteStudentAsync(StudentEntity student, CancellationToken cancellationToken)
    {
        _context.Students.Remove(student);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<StudentEntity?> GetStudentByIdAsync(int id)
    {
        return await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<StudentEntity>> GetStudentsAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<StudentEntity> UpdateStudentAsync(StudentEntity student, CancellationToken cancellationToken)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync(cancellationToken);
        return student;
    }
}
