using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class WorkerCreateRequest
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Guid TitleId { get; set; }
    
    public DateTime HiringDate { get; set; }
    
    public decimal SalaryValue { get; set; }
}