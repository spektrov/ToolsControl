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

[Authorize]
[Route("api/equipments")]
[ApiController]
public class EquipmentsController : BaseApiController
{
    private readonly IEquipmentService _equipmentService;
    private readonly IMapper _mapper;

    public EquipmentsController( IMapper mapper, IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
        _mapper = mapper;
    }
    
    
    /// <summary>
    /// GET api/equipments
    /// </summary>
    /// <param name="parameters">Filter parameters</param>
    /// <returns>200 - Filtered collection</returns>
    [HttpGet]
    public async Task<IActionResult> GetEquipments([FromQuery] EquipmentParameters parameters)
    {
        var units = await _equipmentService.GetEquipments(parameters);
        
        Response.Headers.Add("x-pagination", JsonConvert.SerializeObject(units.MetaData));

        return Ok(await units.ToDynamicListAsync());
    }
    
    
    /// <summary>
    /// GET api/equipments/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">equipment id</param>
    /// <returns>200 - if found; 404 - otherwise</returns>
    [ServiceFilter(typeof(ValidateEquipmentExistsAttribute))]
    [HttpGet("{id:guid}", Name = "GetEquipmentById")]
    public async Task<IActionResult> GetEquipmentById(Guid id)
    {
        var model = HttpContext.Items["equipment"] as EquipmentModel;

        return Ok(model);
    }
    
    
    /// <summary>
    /// POST api/equipments/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="request">Creation model</param>
    /// <returns>201 - if created, 422 - on model error</returns>
    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateEquipment([FromBody]EquipmentCreateRequest request)
    {
        try
        {
            var model = _mapper.Map<EquipmentModel>(request);
            var unitToReturn = await _equipmentService.CreateAsync(model);

            return CreatedAtRoute("GetEquipmentById", 
                new { id = unitToReturn.Id }, unitToReturn); 
        }
        catch (ToolsControlException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    /// <summary>
    /// PUT api/equipments/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">equipment id</param>
    /// <param name="request">Creation model</param>
    /// <returns>204 - if updated, 422 - on model error; 404 - if not found</returns>
    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(ValidateEquipmentExistsAttribute))]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEquipment(Guid id, [FromBody] EquipmentUpdateRequest request)
    {
        var model = _mapper.Map<EquipmentModel>(request);

        try
        {
            await _equipmentService.UpdateAsync(model);

            return NoContent();
        }
        catch (ToolsControlException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    
    /// <summary>
    /// DELETE api/equipments/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">equipment id</param>
    /// <returns>204 - if deleted, 404 - otherwise</returns>
    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidateEquipmentExistsAttribute))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEquipment(Guid id)
    {
        await _equipmentService.DeleteByIdAsync(id);
     
        return NoContent();
    }
}