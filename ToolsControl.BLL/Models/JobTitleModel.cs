namespace ToolsControl.BLL.Models;

public class JobTitleModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public virtual ICollection<WorkerModel>? Workers { get; set; }
}