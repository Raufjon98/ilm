using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Repository;
public class TimeTableRepository : ITimeTableRepository
{
    private readonly IAplicationDbContext _context;
    public TimeTableRepository(IAplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TimeTableEntity> CreateTimeTableAsync(TimeTableEntity timeTable, CancellationToken cancellationToken)
    {
        await _context.TimeTables.AddAsync(timeTable);
        await _context.SaveChangesAsync(cancellationToken);
        return timeTable;
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

    public async Task<TimeTableEntity> UpdateTimeTableAsync(TimeTableEntity timeTable, CancellationToken cancellationToken)
    {
        _context.TimeTables.Update(timeTable);
        await _context.SaveChangesAsync(cancellationToken);
        return timeTable;
    }
}
