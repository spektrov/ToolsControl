﻿namespace ToolsControl.BLL.Models;

public class UserModel
{
    public Guid Id { get; set; }
    
    public string Username { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public DateTime BirthDate { get; set; }
    
    public string? Role { get; set; }
}