using System.Linq.Dynamic.Core;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.WebAPI.ActionFilters;
using ToolsControl.WebAPI.Models;

namespace ToolsControl.WebAPI.Controllers;


[Route("api/accounts")]
[ApiController]
public class AccountsController : BaseApiController
{
     private readonly IUserService _userService;
     private readonly IMapper _mapper;

    public AccountsController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    
    /// <summary>
    /// GET api/accounts
    /// </summary>
    /// <param name="parameters">Filter parameters</param>
    /// <returns>200 - Filtered collection</returns>
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UserParameters parameters)
    {
        var users = await _userService.GetUsersAsync(parameters);
        
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(users.MetaData));

        return Ok(await users.ToDynamicListAsync());
    }

    
    /// <summary>
    /// GET api/accounts/FEBCFCC1-9696-4943-94C8-1032E86B8393
    /// </summary>
    /// <param name="id">Account id</param>
    /// <returns>200 - Account found; 404 - otherwise</returns>
    [ServiceFilter(typeof(ValidateUserExistsAttribute))]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        return Ok(user);
    }
    
    
    /// <summary>
    /// POST api/accounts/register
    /// </summary>
    /// <param name="userSignupRequest">Register model</param>
    /// <returns>204 - if account created; 422 - otherwise</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserSignupRequest userSignupRequest) 
    {
        var user = _mapper.Map<UserModel>(userSignupRequest);
        
        var response = await _userService.SignupAsync(user, userSignupRequest.Password);
        
        if (!response.Success)
        {
            var errors = response.Errors;
                
            return UnprocessableEntity(new {Errors = errors}); 
        }

        return NoContent(); 
    }
    
    
    /// <summary>
    /// PUT api/accounts/FEBCFCC1-9696-4943-94C8-1032E86B8393
    /// </summary>
    /// <param name="id">Account id</param>
    /// <param name="model">Updated model</param>
    /// <returns>200 422 400 codes</returns>
    [ServiceFilter(typeof(ValidateUserExistsAttribute))]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUserInfo(Guid id, [FromBody] UserModel model)
    {
        if (id != model.Id) return NotFound();

        if (Role is "User" or "Manager") model.Id = UserId;
        
        var response = await _userService.UpdateInfoAsync(model);
        if (!response.Success)
        {
            var errors = string.Join('\n', response.Errors!);
            return UnprocessableEntity(errors);
        }

        if (model.Role != null)
        {
            await _userService.AddToRole(id, model.Role);
        }
        
        return Ok();
    }

    
    /// <summary>
    /// DELETE api/accounts/FEBCFCC1-9696-4943-94C8-1032E86B8393
    /// </summary>
    /// <param name="id">Account id</param>
    /// <returns>200 422 404 codes</returns>
    //[Authorize( Roles = "Administrator")]
    [ServiceFilter(typeof(ValidateUserExistsAttribute))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var success = await _userService.DeleteByIdAsync(id);

        return success ? Ok() : UnprocessableEntity();
    }


    /// <summary>
    /// PUT api/accounts/FEBCFCC1-9696-4943-94C8-1032E86B8393/password
    /// </summary>
    /// <param name="id">Account id</param>
    /// <param name="request">Change request</param>
    /// <returns>204 - changed; 400 - otherwise</returns>
    [ServiceFilter(typeof(ValidateUserExistsAttribute))]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPut("{id:guid}/password")]
    public async Task<IActionResult> ChangePassword(Guid id, UserChangePasswordRequest request)
    {
        var response = await _userService.ChangePasswordAsync(id, request.CurrentPassword, request.NewPassword);

        if (!response.Success)
        {
            return BadRequest(response.Errors);
        }

        return NoContent();
    }
}