namespace ilmV3.Domain.interfaces;
public interface IAdminRepository
{
    Task<AdminEntity?> GetAdminByIdAsync(int adminId);
    Task<AdminEntity> UpdateAdminAsync(AdminEntity admin, CancellationToken cancellationToken);
    Task<AdminEntity> CreateAdminAsync(AdminEntity admin, CancellationToken cancellationToken);
    Task<List<AdminEntity>> GetAdminsAsync();
    Task<AdminEntity?> GetAdminAsync(int adminId);
}
