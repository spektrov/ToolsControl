using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class EquipmentTypeCreateRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public int InspectionPeriod { get; set; }
}