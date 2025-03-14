namespace ilmV3.Domain.interfaces;
public interface IApplicationUserRepository
{
    Task<IApplicationUser> CreateUserAsync(int id, string name, string email, string password);
    Task<bool> AddRoleAsync(IApplicationUser user, string role);
}
