using GemBox.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.BLL.Models.Responses;

namespace ToolsControl.WebAPI.Controllers;

[Route("api/statistics")]
[ApiController]
public class StatisticsController : BaseApiController
{
    private readonly IStatisticService _statisticService;
    private readonly IEquipmentReportGenerator<ExcelFile> _reportGenerator;
    
    public StatisticsController(IStatisticService statisticService, IEquipmentReportGenerator<ExcelFile> reportGenerator)
    {
        _statisticService = statisticService;
        _reportGenerator = reportGenerator;
    }


    [HttpGet("most-used")]
    public async Task<IActionResult> GetMostUsed([FromQuery]EquipmentParameters parameters)
    {
        var models = await _statisticService.GetMostUsedEquipment(parameters);

        return Ok(models);
    }
    
    
    [HttpGet("most-used/report")]
    public async Task<IActionResult> GetMostUsedReport([FromQuery]EquipmentParameters parameters)
    {
        var file = await GenerateExcelReport(parameters);

        return new FileContentResult(file.Content, file.ContentType) {FileDownloadName = "download file"};
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
    
    
    
    private async Task<FileResponse> GenerateExcelReport(EquipmentParameters parameters)
    {
        var models = await _statisticService.GetMostUsedEquipment(parameters);

        var file = _reportGenerator.Generate(models, parameters);

        var stream = new MemoryStream();
        var options = new XlsSaveOptions();
        
        file.Save(stream, options);

        return new FileResponse {Content = stream.ToArray(), ContentType = options.ContentType};
    }
}