﻿namespace ToolsControl.BLL.Models;

public class EquipmentTypeModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int InspectionPeriod { get; set; }
    
    public ICollection<EquipmentModel>? Equipments { get; set; }
}