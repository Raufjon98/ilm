using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Repository;
public class GradeRepository : IGradeRepository
{
    private readonly IApplicationDbContext _context;
    public GradeRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateGradeAsync(GradeEntity grade, CancellationToken cancellationToken)
    {
        await _context.Grades.AddAsync(grade);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
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

    public async Task<List<GradeEntity>> GetGradesAsync()
    {
        return await _context.Grades.ToListAsync();
    }

    public async Task<bool> UpdateGradeAsync(GradeEntity grade, CancellationToken cancellationToken)
    {
       _context.Grades.Update(grade);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
