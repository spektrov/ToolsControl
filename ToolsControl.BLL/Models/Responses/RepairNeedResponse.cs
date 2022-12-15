namespace ToolsControl.BLL.Models.Responses;

public class RepairNeedResponse
{
    public string? Name { get; set; }
    
    public string TypeName { get; set; }
    
    public DateTime LastInspection { get; set; }
    
    public bool RepairNeed { get; set; }
}