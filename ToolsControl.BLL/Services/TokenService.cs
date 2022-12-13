using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ToolsControl.BLL.Interfaces;
using ToolsControl.DAL.Entities;
using ToolsControl.DAL.Interfaces;

namespace ToolsControl.BLL.Services;

public class TokenService : ITokenService
{
    private const string LoginProvider = "ToolsControlApp";
    private const string RefreshTokenName = "RefreshToken";
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public TokenService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }


    public async Task<string> GetRefreshTokenAsync(User user)
    {
        if (!await ValidateRefreshToken(user))
        {
            return string.Empty;
        }

        var token = await _unitOfWork.TokenRepository
            .GetAuthenticationTokenAsync(user, LoginProvider, RefreshTokenName);

        return token;
    }
    
    
    public async Task<string> GenerateRefreshTokenAsync(User user)
    {
        await RemoveRefreshTokenAsync(user);
        
        var newRefreshToken = await _unitOfWork.TokenRepository
            .GenerateUserTokenAsync(user, LoginProvider, RefreshTokenName);
        
        await _unitOfWork.TokenRepository.SetAuthenticationTokenAsync(
            user, LoginProvider, RefreshTokenName, newRefreshToken);

        return newRefreshToken;
    }
    
    
    public async Task<bool> RemoveRefreshTokenAsync(User user)
    {
        return await _unitOfWork.TokenRepository
            .RemoveAuthenticationTokenAsync(user, LoginProvider, RefreshTokenName);
    }


    public async Task<string?> UpdateAccessTokenAsync(Guid userId, string refreshToken)
    {
        var user = await _unitOfWork.UserRepository.FindByIdAsync(userId);

        if (user == null || !await ValidateRefreshToken(user))
        {
            return null;
        }

        return await GenerateAccessTokenAsync(user);
    }

    
    public async Task<string> GenerateAccessTokenAsync(User user)
    {
        var signingCredentials = GetSigningCredentials(); 
        var claims = await GetClaims(user); 
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims); 
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    
    
    private async Task<bool> ValidateRefreshToken(User user)
    {
        var refreshToken = await _unitOfWork.TokenRepository
            .GetAuthenticationTokenAsync(user, LoginProvider, RefreshTokenName);
        var isValid = await _unitOfWork.TokenRepository
            .VerifyUserTokenAsync(user, LoginProvider, RefreshTokenName, refreshToken );
        return isValid;
    }


    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Secret").Value!); 
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    
    private async Task<List<Claim>> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        
        var roles = await _unitOfWork.UserRepository.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken (
            issuer: jwtSettings.GetSection("validIssuer").Value, 
            // audience: jwtSettings.GetSection("validAudience").Value, 
            claims: claims, 
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("expires").Value)), 
            signingCredentials: signingCredentials ); 
        
        return tokenOptions;
    }
}