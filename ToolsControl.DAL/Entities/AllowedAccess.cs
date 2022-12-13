namespace ToolsControl.DAL.Entities;

public class AllowedAccess : BaseEntity
{
    public Guid WorkerId { get; set; }
    public Worker? Worker { get; set; }
    
    public Guid EquipmentId { get; set; }
    public Equipment? Equipment { get; set; }
}