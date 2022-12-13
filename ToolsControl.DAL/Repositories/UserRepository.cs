using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public IQueryable<User> FindAll(bool trackChanges)
    {
        return !trackChanges ? 
            _userManager.Users.AsNoTracking() 
            : _userManager.Users;
    }
    
    public async Task<IdentityResult> CreateAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> UpdateAsync(User user)
    {
        return await _userManager.UpdateAsync(user);
    }
    
    public async Task<bool> DeleteAsync(User user)
    {
        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> AddToRoleAsync(User user, string role)
    {
        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> FindByIdAsync(Guid userId)
    {
        return await _userManager.FindByIdAsync(userId.ToString());
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IList<string>> GetRolesAsync(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task RemoveFromAllRoles(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
    }

    public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }
}