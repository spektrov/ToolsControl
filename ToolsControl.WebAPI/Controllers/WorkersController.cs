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

[Route("api/workers")]
[ApiController]
public class WorkersController : BaseApiController
{
     private readonly IWorkerService _workerService;
    private readonly IMapper _mapper;

    public WorkersController( IMapper mapper, IWorkerService workerService)
    {
        _workerService = workerService;
        _mapper = mapper;
    }
    
    
    /// <summary>
    /// GET api/workers
    /// </summary>
    /// <param name="parameters">Filter parameters</param>
    /// <returns>200 - Filtered collection</returns>
    [HttpGet]
    public async Task<IActionResult> GetWorkers([FromQuery] WorkerParameters parameters)
    {
        var units = await _workerService.GetWorkers(parameters);
        
        Response.Headers.Add("x-pagination", JsonConvert.SerializeObject(units.MetaData));

        return Ok(await units.ToDynamicListAsync());
    }
    
    
    /// <summary>
    /// GET api/workers/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">worker id</param>
    /// <returns>200 - if found; 404 - otherwise</returns>
    [ServiceFilter(typeof(ValidateWorkerExistsAttribute))]
    [HttpGet("{id:guid}", Name = "GetWorkerById")]
    public async Task<IActionResult> GetWorkerById(Guid id)
    {
        var model = HttpContext.Items["worker"] as WorkerModel;

        return Ok(model);
    }
    
    
    /// <summary>
    /// POST api/workers/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="request">Creation model</param>
    /// <returns>201 - if created, 422 - on model error</returns>
   // [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateWorker([FromBody]WorkerCreateRequest request)
    {
        try
        {
            var model = _mapper.Map<WorkerModel>(request);
            var unitToReturn = await _workerService.CreateAsync(model);

            return CreatedAtRoute("GetWorkerById", 
                new { id = unitToReturn.Id }, unitToReturn); 
        }
        catch (ToolsControlException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    /// <summary>
    /// PUT api/workers/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">worker id</param>
    /// <param name="request">Creation model</param>
    /// <returns>204 - if updated, 422 - on model error; 404 - if not found</returns>
   // [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(ValidateWorkerExistsAttribute))]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateWorker(Guid id, [FromBody] WorkerUpdateRequest request)
    {
        var model = _mapper.Map<WorkerModel>(request);

        try
        {
            await _workerService.UpdateAsync(model);

            return NoContent();
        }
        catch (ToolsControlException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    
    /// <summary>
    /// DELETE api/workers/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">worker id</param>
    /// <returns>204 - if deleted, 404 - otherwise</returns>
   // [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidateWorkerExistsAttribute))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteWorker(Guid id)
    {
        await _workerService.DeleteByIdAsync(id);
     
        return NoContent();
    }
}