using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class UserLoginRequest
{
    [Required(ErrorMessage = "Email is required")] 
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password name is required")]
    public string Password { get; set; } = string.Empty;
}