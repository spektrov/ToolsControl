using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class UsageUpdateRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string WorkerCard { get; set; }

    [Required]
    public Guid EquipmentId { get; set; }

    [Required]
    public DateTime Start { get; set; }
    
    public DateTime? Finish { get; set; }
}