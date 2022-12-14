using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToolsControl.BLL.Exceptions;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models;
using ToolsControl.WebAPI.Models;

namespace ToolsControl.WebAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthController(IMapper mapper, IUserService userService, ITokenService tokenService)
    {
        _mapper = mapper;
        _userService = userService;
        _tokenService = tokenService;
    }
    


    /// <summary>
    /// POST api/auth/login
    /// </summary>
    /// <param name="loginRequest">Login model</param>
    /// <returns>200 - generated tokens; 400</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest loginRequest)
    {
        try
        {
            var tokens = await _userService.SigninAsync(loginRequest.Email, loginRequest.Password);
            return Ok(tokens);
        }
        catch (ToolsControlException exception)
        {
            return BadRequest(exception.Message);
        }
    }
    

    /// <summary>
    /// POST api/auth/refresh-token
    /// </summary>
    /// <param name="request">Refresh token model</param>
    /// <returns>200 - generated tokens; 400</returns>
    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var accessTokenUpdated = await _tokenService.UpdateAccessTokenAsync(request.UserId, request.RefreshToken);

        if (accessTokenUpdated == null)
        {
            await _userService.LogoutAsync(request.UserId);
            return BadRequest("Cannot update refresh token");
        }
        
        return Ok(new { AccessToken = accessTokenUpdated, request.RefreshToken });
    }
    
    
    /// <summary>
    /// POST api/auth/logout
    /// </summary>
    /// <returns>204 - success logout; 400 - otherwise</returns>
    [Authorize]
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        var success = await _userService.LogoutAsync(UserId);
        
        return success 
            ? NoContent() 
            : BadRequest();
    }
}