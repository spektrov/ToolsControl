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

[Route("api/job-titles")]
[ApiController]
public class JobTitlesController : BaseApiController
{
    private readonly IJobTitleService _jobTitleService;
    private readonly IMapper _mapper;

    public JobTitlesController(IMapper mapper, IJobTitleService jobTitleService)
    {
        _jobTitleService = jobTitleService;
        _mapper = mapper;
    }
    
    
    /// <summary>
    /// GET api/job-titles
    /// </summary>
    /// <param name="parameters">Filter parameters</param>
    /// <returns>200 - Filtered collection</returns>
    [HttpGet]
    public async Task<IActionResult> GetJobTitles([FromQuery] JobTitleParameters parameters)
    {
        var units = await _jobTitleService.GetJobTitles(parameters);
        
        Response.Headers.Add("x-pagination", JsonConvert.SerializeObject(units.MetaData));

        return Ok(await units.ToDynamicListAsync());
    }
    
    
    /// <summary>
    /// GET api/job-titles/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">job title id</param>
    /// <returns>200 - if found; 404 - otherwise</returns>
    [ServiceFilter(typeof(ValidateJobTitleExistsAttribute))]
    [HttpGet("{id:guid}", Name = "GetJobTitleById")]
    public async Task<IActionResult> GetJobTitleById(Guid id)
    {
        var model = HttpContext.Items["jobTitle"] as JobTitleModel;

        return Ok(model);
    }
    
    
    /// <summary>
    /// POST api/job-titles/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="request">Creation model</param>
    /// <returns>201 - if created, 422 - on model error</returns>
   // [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateJobTitle([FromBody]JobTitleCreateRequest request)
    {
        try
        {
            var model = _mapper.Map<JobTitleModel>(request);
            var unitToReturn = await _jobTitleService.CreateAsync(model);

            return CreatedAtRoute("GetJobTitleById", 
                new { id = unitToReturn.Id }, unitToReturn); 
        }
        catch (ToolsControlException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    /// <summary>
    /// PUT api/job-titles/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">job title id</param>
    /// <param name="request">Creation model</param>
    /// <returns>204 - if updated, 422 - on model error; 404 - if not found</returns>
    //[Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(ValidateJobTitleExistsAttribute))]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateJobTitle(Guid id, [FromBody] JobTitleUpdateRequest request)
    {
        var model = _mapper.Map<JobTitleModel>(request);

        try
        {
            await _jobTitleService.UpdateAsync(model);

            return NoContent();
        }
        catch (ToolsControlException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    
    /// <summary>
    /// DELETE api/job-titles/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">job title id</param>
    /// <returns>204 - if deleted, 404 - otherwise</returns>
    //[Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidateJobTitleExistsAttribute))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteJobTitle(Guid id)
    {
        await _jobTitleService.DeleteByIdAsync(id);
     
        return NoContent();
    }
}