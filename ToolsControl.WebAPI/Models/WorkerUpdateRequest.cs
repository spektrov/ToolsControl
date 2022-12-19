using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class WorkerUpdateRequest
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid TitleId { get; set; }
    
    public string CardNumber { get; set; } = string.Empty;
    
    public DateTime HiringDate { get; set; }
    
    public decimal SalaryValue { get; set; }
}