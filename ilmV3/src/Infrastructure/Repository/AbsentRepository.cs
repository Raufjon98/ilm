using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Domain.Entities;
using ilmV3.Domain.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ilmV3.Infrastructure.Repository;
public class AbsentRepository : IAbsentRepository
{
    private readonly IApplicationDbContext _context;

    public AbsentRepository(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> CreateAbsentAsync(AbsentEntity entity, CancellationToken cancellationToken)
    {
        _context.Absents.Add(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
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

    public async Task<bool> UpdateAbsentAsync(AbsentEntity entity, CancellationToken cancellationToken)
    {
        _context.Absents.Update(entity);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

}
