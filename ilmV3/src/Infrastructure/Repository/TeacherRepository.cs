using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Repository;
public class TeacherRepository : ITeacherRepository
{
    private readonly IAplicationDbContext _context;
    public TeacherRepository(IAplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TeacherEntity> CreateTeacherAsync(TeacherEntity teacher, CancellationToken cancellationToken)
    {

        await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync(cancellationToken);
        return teacher;
    }

    public async Task<bool> DeleteTeacherAsync(TeacherEntity teacher, CancellationToken cancellationToken)
    {
        _context.Teachers.Remove(teacher);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<TeacherEntity?> GetTeacherByIdAsync(int id)
    {
        return await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TeacherEntity> UpdateTeacherAsync(TeacherEntity teacher, CancellationToken cancellationToken)
    {
        _context.Teachers.Update(teacher);
        await _context.SaveChangesAsync(cancellationToken);
        return teacher;
    }
}
