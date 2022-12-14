using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToolsControl.BLL.Interfaces;

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
    
    
}