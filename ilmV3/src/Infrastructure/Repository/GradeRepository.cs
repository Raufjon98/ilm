using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Repository;
public class GradeRepository : IGradeRepository
{
    private readonly IAplicationDbContext _context;
    public GradeRepository(IAplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GradeEntity> CreateGradeAsync(GradeEntity grade, CancellationToken cancellationToken)
    {
        await _context.Grades.AddAsync(grade);
        await _context.SaveChangesAsync(cancellationToken);
        return grade;
    }

    public async Task<bool> DeleteGradeAsync(GradeEntity grade, CancellationToken cancellationToken)
    {
        _context.Grades.Remove(grade);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<GradeEntity?> GetGradeByIdAsync(int id)
    {
        return await _context.Grades.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<GradeEntity> UpdateGradeAsync(GradeEntity grade, CancellationToken cancellationToken)
    {
        _context.Grades.Update(grade);
        await _context.SaveChangesAsync(cancellationToken);
        return grade;
    }
}
