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
[Route("api/equipment-types")]
[ApiController]
public class EquipmentTypesController : BaseApiController
{
    private readonly IEquipmentTypeService _equipmentTypeService;
    private readonly IMapper _mapper;

    public EquipmentTypesController(IMapper mapper, IEquipmentTypeService equipmentTypeService)
    {
        _equipmentTypeService = equipmentTypeService;
        _mapper = mapper;
    }
    
    
    /// <summary>
    /// GET api/equipment-types
    /// </summary>
    /// <param name="parameters">Filter parameters</param>
    /// <returns>200 - Filtered collection</returns>
    [HttpGet]
    public async Task<IActionResult> GetEquipmentTypes([FromQuery] EquipmentTypeParameters parameters)
    {
        var units = await _equipmentTypeService.GetEquipmentTypes(parameters);
        
        Response.Headers.Add("x-pagination", JsonConvert.SerializeObject(units.MetaData));

        return Ok(await units.ToDynamicListAsync());
    }
    
    
    /// <summary>
    /// GET api/equipment-types/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">equipment type id</param>
    /// <returns>200 - if found; 404 - otherwise</returns>
    [ServiceFilter(typeof(ValidateEquipmentTypeExistsAttribute))]
    [HttpGet("{id:guid}", Name = "GetEquipmentTypeById")]
    public async Task<IActionResult> GetEquipmentTypeById(Guid id)
    {
        var model = HttpContext.Items["equipmentType"] as EquipmentTypeModel;

        return Ok(model);
    }
    
    
    /// <summary>
    /// POST api/equipment-types/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="request">Creation model</param>
    /// <returns>201 - if created, 422 - on model error</returns>
    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [HttpPost]
    public async Task<IActionResult> CreateEquipmentType([FromBody]EquipmentTypeCreateRequest request)
    {
        try
        {
            var model = _mapper.Map<EquipmentTypeModel>(request);
            var unitToReturn = await _equipmentTypeService.CreateAsync(model);

            return CreatedAtRoute("GetEquipmentTypeById", 
                new { id = unitToReturn.Id }, unitToReturn); 
        }
        catch (ToolsControlException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    /// <summary>
    /// PUT api/equipment-types/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">equipment type id</param>
    /// <param name="request">Creation model</param>
    /// <returns>204 - if updated, 422 - on model error; 404 - if not found</returns>
    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(ValidateEquipmentTypeExistsAttribute))]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEquipmentType(Guid id, [FromBody] EquipmentTypeUpdateRequest request)
    {
        var model = _mapper.Map<EquipmentTypeModel>(request);

        try
        {
            await _equipmentTypeService.UpdateAsync(model);

            return NoContent();
        }
        catch (ToolsControlException e)
        {
            return UnprocessableEntity(e.Message);
        }
    }

    
    /// <summary>
    /// DELETE api/equipment-types/EBBC042A-50F8-4B35-B72C-3839AB885455
    /// </summary>
    /// <param name="id">equipment type id</param>
    /// <returns>204 - if deleted, 404 - otherwise</returns>
    [Authorize(Roles = "Administrator")]
    [ServiceFilter(typeof(ValidateEquipmentTypeExistsAttribute))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEquipmentType(Guid id)
    {
        await _equipmentTypeService.DeleteByIdAsync(id);
     
        return NoContent();
    }
}