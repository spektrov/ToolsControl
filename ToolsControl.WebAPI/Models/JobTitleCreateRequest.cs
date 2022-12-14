using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class JobTitleCreateRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}