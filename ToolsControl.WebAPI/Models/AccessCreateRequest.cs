using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class AccessCreateRequest
{
    [Required]
    public Guid WorkerId { get; set; }
    
    [Required]
    public Guid EquipmentId { get; set; }
}