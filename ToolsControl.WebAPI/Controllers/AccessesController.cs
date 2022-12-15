using System.Linq.Dynamic.Core;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ToolsControl.BLL.Exceptions;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.WebAPI.ActionFilters;
using ToolsControl.WebAPI.Models;

namespace ToolsControl.WebAPI.Controllers;


[Route("api/accesses")]
[ApiController]
public class AccessesController : BaseApiController
{
    private readonly IAccessService _accessService;
    private readonly IMapper _mapper;

    public AccessesController( IMapper mapper, IAccessService accessService)
    {
        _accessService = accessService;
        _mapper = mapper;
    }
    
    
     /// <summary>
    /// GET api/accesses
    /// </summary>
    /// <param name="parameters">Filter parameters</param>
    /// <returns>200 - Filtered collection</returns>
    [HttpGet]
    public async Task<IActionResult> GetAccesses([FromQuery] AllowedAccessParameters parameters)
    {
        var units = await _accessService.GetAccesses(parameters);
        
        Response.Headers.Add("x-pagination", JsonConvert.SerializeObject(units.MetaData));

        return Ok(await units.ToDynamicListAsync());
    }
    
    
    /// <summary>
    /// GET api/accesses/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">access id</param>
    /// <returns>200 - if found; 404 - otherwise</returns>
    [ServiceFilter(typeof(ValidateAccessExistsAttribute))]
    [HttpGet("{id:guid}", Name = "GetAccessById")]
    public async Task<IActionResult> GetAccessById(Guid id)
    {
        var model = HttpContext.Items["access"] as AllowedAccessModel;

        return Ok(model);
    }
    
    
    /// <summary>
    /// POST api/accesses/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="request">Creation model</param>
    /// <returns>201 - if created, 422 - on model error</returns>
    //[Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateAccess([FromBody]AccessCreateRequest request)
    {
        try
        {
            var model = _mapper.Map<AllowedAccessModel>(request);
            var unitToReturn = await _accessService.CreateAsync(model);

            return CreatedAtRoute("GetAccessById", 
                new { id = unitToReturn.Id }, unitToReturn); 
        }
        catch (ToolsControlException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }
    
    
    /// <summary>
    /// DELETE api/accesses/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">access id</param>
    /// <returns>204 - if deleted, 404 - otherwise</returns>
   // [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidateAccessExistsAttribute))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAccess(Guid id)
    {
        await _accessService.DeleteByIdAsync(id);
     
        return NoContent();
    }
}