using Microsoft.AspNetCore.Identity;
using ToolsControl.DAL.Entities;

namespace ToolsControl.DAL.Interfaces;

public interface IUserRepository
{
    public IQueryable<ApplicationUser> FindAll(bool trackChanges);

    public Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
    
    public Task<IdentityResult> UpdateAsync(ApplicationUser user);
    
    public Task<bool> DeleteAsync(ApplicationUser user);
    
    public Task<bool> AddToRoleAsync(ApplicationUser user, string role);

    public Task<ApplicationUser?> FindByEmailAsync(string email);
    
    public Task<ApplicationUser?> FindByIdAsync(Guid userId);

    public Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

    public Task<IList<string>> GetRolesAsync(ApplicationUser user);

    public Task RemoveFromAllRoles(ApplicationUser user);

    public Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
}