using ilmV3.Application.Account.Commands.Register;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.interfaces;


namespace ilmV3.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<bool> AuthorizeAsync(string userId, string policyName);
    Task<IApplicationUser?> CreateUserAsync(int externalUserId, RegisterDto register, string role);
    Task<IApplicationUser?> GetUserByIdAsync(string userId);
    Task<IApplicationUser?> GetUserByUsernameAsync(string username);
    Task<bool> CheckPasswordAsync(IApplicationUser user, string password);
    Task<Result> DeleteUserAsync(string userId);
    Task<IApplicationUser> UpdateUserAsync(IApplicationUser user);
}
