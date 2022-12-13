using Microsoft.AspNetCore.Identity;
using ToolsControl.DAL.Entities;

namespace ToolsControl.DAL.Interfaces;

public interface IUserRepository
{
    public IQueryable<User> FindAll(bool trackChanges);

    public Task<IdentityResult> CreateAsync(User user, string password);
    
    public Task<IdentityResult> UpdateAsync(User user);
    
    public Task<bool> DeleteAsync(User user);
    
    public Task<bool> AddToRoleAsync(User user, string role);

    public Task<User?> FindByEmailAsync(string email);
    
    public Task<User?> FindByIdAsync(Guid userId);

    public Task<bool> CheckPasswordAsync(User user, string password);

    public Task<IList<string>> GetRolesAsync(User user);

    public Task RemoveFromAllRoles(User user);

    public Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);
}