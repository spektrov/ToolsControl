namespace ToolsControl.BLL.Models.Responses;

public class MostUsedEquipmentResponse
{
    public bool IsAbleToWork { get; set; }
    
    public DateTime LastInspection { get; set; }
    
    public string? Name { get; set; }
    
    public string? TypeName { get; set; }
    
    public decimal Time { get; set; }
}