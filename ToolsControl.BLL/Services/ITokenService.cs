using ToolsControl.DAL.Entities;

namespace ToolsControl.BLL.Services;

public interface ITokenService
{
    public Task<string> GenerateAccessTokenAsync(User user);

    public Task<string?> UpdateAccessTokenAsync(Guid userId, string refreshToken);

    public Task<string> GenerateRefreshTokenAsync(User user);
    
    public Task<string> GetRefreshTokenAsync(User user);

    public Task<bool> RemoveRefreshTokenAsync(User user);
}