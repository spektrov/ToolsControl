namespace ToolsControl.DAL.Entities;

public class JobTitle : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    
    public virtual ICollection<Worker>? Workers { get; set; }
}