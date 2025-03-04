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
public class TimeTableRepository : ITimeTableRepository
{
    private readonly IApplicationDbContext _context;
    public TimeTableRepository(IApplicationDbContext context)
    {
         _context = context;
    }

    public async Task<bool> CreateTimeTableAsync(TimeTableEntity timeTable, CancellationToken cancellationToken)
    {
        await _context.TimeTables.AddAsync(timeTable);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> DeleteTimeTableAsync(TimeTableEntity timeTable, CancellationToken cancellationToken)
    {
        _context.TimeTables.Remove(timeTable);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<TimeTableEntity?> GetTimeTableByIdAsync(int id)
    {
        return await _context.TimeTables.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<TimeTableEntity>> GetTimeTablesAsync()
    {
        return await _context.TimeTables.ToListAsync();
    }

    public async Task<bool> UpdateTimeTableAsync(TimeTableEntity timeTable, CancellationToken cancellationToken)
    {
        _context.TimeTables.Update(timeTable);
        return await _context.SaveChangesAsync(cancellationToken) > 0; 
    }
}
