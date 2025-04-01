using ilmV3.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using ilmV3.Domain.Entities;
using ilmV3.Application.Common.Interfaces;

namespace ilmV3.Infrastructure.Repository;
public class AdminRepository : IAdminRepository
{
    private readonly IApplicationDbContext _context;

    public AdminRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AdminEntity> CreateAdminAsync(AdminEntity admin, CancellationToken cancellationToken)
    {
        await _context.Admins.AddAsync(admin);
        await _context.SaveChangesAsync(cancellationToken);
        return admin;
    }

    public async Task<bool> DeleteAdminAsync(AdminEntity admin, CancellationToken cancellationToken)
    {
        _context.Admins.Remove(admin);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<AdminEntity?> GetAdminAsync(int adminId)
    {
        return await _context.Admins.FindAsync(adminId);
    }

    public async Task<AdminEntity?> GetAdminByIdAsync(int adminId)
    {
        var result = await _context.Admins.FirstOrDefaultAsync(x => x.Id == adminId);
        return result;
    }

    public async Task<List<AdminEntity>> GetAdminsAsync()
    {
        return await _context.Admins.ToListAsync();
    }

    public async Task<AdminEntity> UpdateAdminAsync(AdminEntity admin, CancellationToken cancellationToken)
    {
        _context.Admins.Update(admin);
        await _context.SaveChangesAsync(cancellationToken);
        return admin;
    }
}
