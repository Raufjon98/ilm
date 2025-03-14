using ilmV3.Domain.interfaces;
using ilmV3.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace ilmV3.Infrastructure.Repository;
public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationUserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<bool> AddRoleAsync(IApplicationUser user, string role)
    {
        var result = await _userManager.AddToRoleAsync((ApplicationUser)user, role);
        return result.Succeeded;
    }

    public async Task<IApplicationUser> CreateUserAsync(int id, string name, string email, string password)
    {
        var user = new ApplicationUser
        {
            ExternalUserId = id,
            Email = email,
            UserName = name

        };
        await _userManager.CreateAsync(user, password);
        return user;
    }
}
