namespace ToolsControl.BLL.Models;

public class EquipmentModel
{
    public Guid Id { get; set; }
    
    public bool IsAvailable { get; set; }
    
    public bool IsAbleToWork { get; set; }
    
    public DateTime LastInspection { get; set; }
    
    public string? Name { get; set; }

    public Guid TypeId { get; set; }
    public EquipmentTypeModel? Type { get; set; }

    public virtual ICollection<AllowedAccessModel>? AllowedAccesses { get; set; }
    
    public virtual ICollection<UsageModel>? Usages { get; set; }
}