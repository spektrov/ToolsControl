namespace ToolsControl.BLL.Models.RequestFeatures;

public class EquipmentParameters : RequestParameters
{
    public string? SearchTerm { get; set; }
    
    public string? TypeName { get; set; }
    
    
    public DateTime? StartPeriod { get; set; }
    
    public DateTime? EndPeriod { get; set; }
}