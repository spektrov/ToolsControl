namespace ToolsControl.DAL.Entities;

public class EquipmentType : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public int InspectionPeriod { get; set; }
    
    public ICollection<Equipment>? Equipments { get; set; }
}