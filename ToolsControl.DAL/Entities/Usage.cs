namespace ToolsControl.DAL.Entities;

public class Usage : BaseEntity
{
    public Guid WorkerId { get; set; }
    public Worker? Worker { get; set; }
    
    public Guid EquipmentId { get; set; }
    public Equipment? Equipment { get; set; }
    
    public DateTime Start { get; set; }
    
    public DateTime? Finish { get; set; }
}