using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class UsageCreateRequest
{
    [Required]
    public Guid WorkerId { get; set; }

    [Required]
    public Guid EquipmentId { get; set; }

    [Required]
    public DateTime Start { get; set; }
    
    public DateTime? Finish { get; set; }
}