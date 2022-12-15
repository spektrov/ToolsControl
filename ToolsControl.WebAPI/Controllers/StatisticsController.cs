using Microsoft.AspNetCore.Mvc;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models.RequestFeatures;

namespace ToolsControl.WebAPI.Controllers;

[Route("api/statistics")]
[ApiController]
public class StatisticsController : BaseApiController
{
    private readonly IStatisticService _statisticService;

    public StatisticsController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }


    [HttpGet("most-used")]
    public async Task<IActionResult> GetMostUsed([FromQuery]EquipmentParameters parameters)
    {
        var models = await _statisticService.GetMostUsedEquipment(parameters);

        return Ok(models);
    }
    
    [HttpGet("repair-need")]
    public async Task<IActionResult> GetRepairNeed([FromQuery]EquipmentParameters parameters)
    {
        var models = await _statisticService.GetRepairNeed(parameters);

        return Ok(models);
    }
    
    [HttpGet("workers-time")]
    public async Task<IActionResult> GetWorkersTime([FromQuery]WorkerParameters parameters)
    {
        var models = await _statisticService.GetWorkersWorkTime(parameters);

        return Ok(models);
    }
}