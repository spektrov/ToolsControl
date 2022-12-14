using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class UserChangePasswordRequest
{
    [Required]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required]
    public string NewPassword { get; set; } = string.Empty;
}