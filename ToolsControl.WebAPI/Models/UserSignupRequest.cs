using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class UserSignupRequest
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")] 
    public string Password { get; set; }  = string.Empty;

    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public string? Role { get; set; }
}