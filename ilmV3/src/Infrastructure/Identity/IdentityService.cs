using System.Globalization;
using ilmV3.Application.Account.Commands.Register;
using ilmV3.Application.Common.Interfaces;
using ilmV3.Application.Common.Models;
using ilmV3.Domain.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ilmV3.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _signInManager = signInManager;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<IApplicationUser?> CreateUserAsync(int externalUserId, RegisterDto register, string role)
    {
        ArgumentNullException.ThrowIfNull(register);

        ApplicationUser user = new ApplicationUser
        {
            UserName = register.UserName,
            Email = register.Email,
            ExternalUserId = externalUserId,
        };

        var createdUser = await _userManager.CreateAsync(user, register.Password);
        if (!createdUser.Succeeded)
        {
            throw new Exception($"User creation failed: {string.Join(", ", createdUser.Errors.Select(e => $"{e.Code} - {e.Description}"))}");
        }

        var result = await _userManager.AddToRoleAsync(user, role);
        if (!result.Succeeded)
        {
            throw new Exception($"Role Addition failed: {string.Join(",", result.Errors.Select(e => $"{e.Code} - {e.Description}"))}");
        }
        return user;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }


    public async Task<IApplicationUser?> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new Exception("User not found!");
        }

        return user;
    }

    public async Task<IApplicationUser?> GetUserByUsernameAsync(string username)
    {
        var user = await _userManager.FindByEmailAsync(username);
        if (user == null)
        {
            throw new Exception($"User by username: {username} not found!");
        }
        return user;
    }

    public async Task<bool> CheckPasswordAsync(IApplicationUser user, string password)
    {
        var resul = await _signInManager.CheckPasswordSignInAsync((ApplicationUser)user, password, false);
        return resul.Succeeded;
    }

    public async Task<IApplicationUser> UpdateUserAsync(IApplicationUser user)
    {
        var result = await _userManager.UpdateAsync((ApplicationUser)user);
        ArgumentNullException.ThrowIfNull(result);
        return user;
    }
}
