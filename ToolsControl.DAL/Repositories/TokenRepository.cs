using Microsoft.AspNetCore.Identity;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.DAL.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly UserManager<User> _userManager;

    public TokenRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }


    public async Task<string> GetAuthenticationTokenAsync(User user, string loginProvider, string tokenName)
    {
        return await _userManager.GetAuthenticationTokenAsync(user, loginProvider, tokenName);
    }

    public async Task<bool> SetAuthenticationTokenAsync(User user, string loginProvider, string tokenName, string tokenValue)
    {
        var result = await _userManager.SetAuthenticationTokenAsync(user, loginProvider, tokenName, tokenValue);
        return result.Succeeded;
    }

    public async Task<bool> RemoveAuthenticationTokenAsync(User user, string loginProvider, string tokenName)
    {
        var result = await _userManager.RemoveAuthenticationTokenAsync(user, loginProvider, tokenName);
        return result.Succeeded;
    }

    public async Task<string> GenerateUserTokenAsync(User user, string tokenProvider, string purpose)
    {
        return await _userManager.GenerateUserTokenAsync(user, tokenProvider, purpose);
    }

    public async Task<bool> VerifyUserTokenAsync(User user, string loginProvider, string tokenName, string tokenValue)
    { 
        var result = await _userManager.VerifyUserTokenAsync(user, loginProvider, tokenName, tokenValue);
        return result;
    }
}