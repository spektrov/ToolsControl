namespace ToolsControl.BLL.Models.RequestFeatures;

public class WorkerParameters : RequestParameters
{
    public string? JobTitle { get; set; }
    
    public DateTime StartPeriod { get; set; }
    
    public DateTime EndPeriod { get; set; }
}