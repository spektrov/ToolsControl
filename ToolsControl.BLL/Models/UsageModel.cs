namespace ToolsControl.BLL.Models;

public class UsageModel
{
    public Guid Id { get; set; }

    public Guid WorkerId { get; set; }
    public WorkerModel? Worker { get; set; }
    
    public Guid EquipmentId { get; set; }
    public EquipmentModel? Equipment { get; set; }
    
    public DateTime Start { get; set; }
    
    public DateTime? Finish { get; set; }
}