namespace ToolsControl.BLL.Models;

public class AllowedAccessModel
{
    public Guid Id { get; set; }
    
    public Guid WorkerId { get; set; }
    public WorkerModel? Worker { get; set; }
    
    public Guid EquipmentId { get; set; }
    public EquipmentModel? Equipment { get; set; }
}