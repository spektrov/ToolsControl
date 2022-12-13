namespace ToolsControl.DAL.Entities;

public class Equipment : BaseEntity
{
    public bool IsAvailable { get; set; }
    
    public bool IsAbleToWork { get; set; }
    
    public DateTime LastInspection { get; set; }
    
    public string? Name { get; set; }

    public Guid TypeId { get; set; }
    public EquipmentType? Type { get; set; }

    public virtual ICollection<AllowedAccess>? AllowedAccesses { get; set; }
    
    public virtual ICollection<Usage>? Usages { get; set; }
}