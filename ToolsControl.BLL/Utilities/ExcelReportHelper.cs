using GemBox.Spreadsheet;

namespace ToolsControl.BLL.Utilities;

public static class ExcelReportHelper
{
    public static ExcelFile InitialSetup()
    {
        SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        return new ExcelFile();
    }
    
    
    public static void AfterAllSetup(ExcelWorksheet table)
    {
        for (int i = 0; i < table.CalculateMaxUsedColumns(); i++)
        {
            table.Columns[i].AutoFit(1, table.Rows[0], table.Rows[^1]);
        }
    }
    
    
    public static void SetThinkBorder(ExcelWorksheet table, IEnumerable<string> cells)
    {
        foreach (var cell in cells)
        {
            table.Cells[cell].Style.Borders
                .SetBorders(MultipleBorders.Bottom, SpreadsheetColor.FromArgb(0, 0, 0), LineStyle.Thick);
        }
    }
}