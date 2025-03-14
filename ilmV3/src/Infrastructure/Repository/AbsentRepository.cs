using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Repository;
public class AbsentRepository : IAbsentRepository
{
    private readonly IApplicationDbContext _context;

    public AbsentRepository(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<AbsentEntity> CreateAbsentAsync(AbsentEntity absent, CancellationToken cancellationToken)
    {
        var result = _context.Absents.Add(absent);
        await _context.SaveChangesAsync(cancellationToken);
        return absent;
    }

    public async Task<bool> DeleteAbsentAsync(AbsentEntity entity, CancellationToken cancellationToken)
    {
        _context.Absents.Remove(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;

    }

    public async Task<AbsentEntity?> GetAbsentByIdAsync(int id)
    {
        return await _context.Absents.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<AbsentEntity>> GetAbsentsAsync()
    {
        return await _context.Absents.ToListAsync();
    }

    public async Task<AbsentEntity> UpdateAbsentAsync(AbsentEntity absent, CancellationToken cancellationToken)
    {
        _context.Absents.Update(absent);
        await _context.SaveChangesAsync(cancellationToken);
        return absent;
    }

}
