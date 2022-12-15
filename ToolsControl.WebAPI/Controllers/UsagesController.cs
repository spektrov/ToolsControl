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

[Route("api/usages")]
[ApiController]
public class UsagesController : BaseApiController
{
    private readonly IUsageService _usageService;
    private readonly IMapper _mapper;

    public UsagesController( IMapper mapper, IUsageService usageService)
    {
        _usageService = usageService;
        _mapper = mapper;
    }
    
    
     /// <summary>
    /// GET api/usages
    /// </summary>
    /// <param name="parameters">Filter parameters</param>
    /// <returns>200 - Filtered collection</returns>
    [HttpGet]
    public async Task<IActionResult> GetUsages([FromQuery] UsageParameters parameters)
    {
        var units = await _usageService.GetUsages(parameters);
        
        Response.Headers.Add("x-pagination", JsonConvert.SerializeObject(units.MetaData));

        return Ok(await units.ToDynamicListAsync());
    }
    
    
    /// <summary>
    /// GET api/usages/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">usage id</param>
    /// <returns>200 - if found; 404 - otherwise</returns>
    [ServiceFilter(typeof(ValidateUsageExistsAttribute))]
    [HttpGet("{id:guid}", Name = "GetUsageById")]
    public async Task<IActionResult> GetUsageById(Guid id)
    {
        var model = HttpContext.Items["usage"] as UsageModel;

        return Ok(model);
    }
    
    
    /// <summary>
    /// POST api/usages/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="request">Creation model</param>
    /// <returns>201 - if created, 422 - on model error</returns>
    //[Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateUsage([FromBody]UsageCreateRequest request)
    {
        try
        {
            var model = _mapper.Map<UsageModel>(request);
            var unitToReturn = await _usageService.CreateAsync(model);

            return CreatedAtRoute("GetUsageById", 
                new { id = unitToReturn.Id }, unitToReturn); 
        }
        catch (ToolsControlException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    /// <summary>
    /// PUT api/usages/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">usages id</param>
    /// <param name="request">Creation model</param>
    /// <returns>204 - if updated, 422 - on model error; 404 - if not found</returns>
   // [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(ValidateUsageExistsAttribute))]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateUsage(Guid id, [FromBody] UsageUpdateRequest request)
    {
        var model = _mapper.Map<UsageModel>(request);

        try
        {
            await _usageService.UpdateAsync(model);

            return NoContent();
        }
        catch (ToolsControlException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    
    /// <summary>
    /// DELETE api/usages/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">usage id</param>
    /// <returns>204 - if deleted, 404 - otherwise</returns>
   // [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidateUsageExistsAttribute))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUsage(Guid id)
    {
        await _usageService.DeleteByIdAsync(id);
     
        return NoContent();
    }
}