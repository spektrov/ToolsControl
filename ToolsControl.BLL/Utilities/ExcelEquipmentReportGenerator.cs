using GemBox.Spreadsheet;
using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Models.RequestFeatures;
using ToolsControl.BLL.Models.Responses;

namespace ToolsControl.BLL.Utilities;

public class ExcelEquipmentReportGenerator : IEquipmentReportGenerator<ExcelFile>
{
    public ExcelFile Generate(IEnumerable<MostUsedEquipmentResponse> models, EquipmentParameters parameters)
    {
        var spreadsheet = ExcelReportHelper.InitialSetup();
        var table = spreadsheet.Worksheets.Add($"user_knowledge_{DateTime.Now.Millisecond}");

        table.Cells[0, 0].Value = "Звіт згереровано:";
        table.Cells[0, 1].Value = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        
        GenerateData(table, models);
        
        ExcelReportHelper.AfterAllSetup(table);

        return spreadsheet;
    }
    
    
    private void GenerateData(ExcelWorksheet table, IEnumerable<MostUsedEquipmentResponse> models)
    {
        var units = models.ToList();

        ExcelReportHelper.SetThinkBorder(table, new List<string> { "A3", "B3", "C3", "D3", "E3" });
        
        table.Cells[2, 0].Value = "Назва";
        table.Cells[2, 1].Value = "Тип";
        table.Cells[2, 2].Value = "Справний стан";
        table.Cells[2, 3].Value = "Дата останнього огляду";
        table.Cells[2, 4].Value = "Час у роботі(год)";

        const int row = 3;
        for (var i = 0; i < units.Count; i++)
        {
            table.Cells[i + row, 0].Value = units[i].Name;
            table.Cells[i + row, 1].Value = units[i].TypeName;
            table.Cells[i + row, 2].Value = units[i].IsAbleToWork ? "Так" : "Ні";
            table.Cells[i + row, 3].Value = units[i].LastInspection.ToString("dd.MM.yyyy");
            table.Cells[i + row, 4].Value = units[i].Time;
        }
    }
}