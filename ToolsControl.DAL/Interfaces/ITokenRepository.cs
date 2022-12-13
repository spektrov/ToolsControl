using ToolsControl.DAL.Entities;

namespace ToolsControl.DAL.Interfaces;

public interface ITokenRepository
{
    public Task<string> GetAuthenticationTokenAsync(User user, string loginProvider, string tokenName);

    public Task<bool> SetAuthenticationTokenAsync(
        User user, string loginProvider, string tokenName, string tokenValue);

    public Task<bool> RemoveAuthenticationTokenAsync(User user, string loginProvider, string tokenName);

    public Task<string> GenerateUserTokenAsync(User user, string tokenProvider, string purpose);

    public Task<bool> VerifyUserTokenAsync(User user, string loginProvider, string tokenName, string tokenValue);
}