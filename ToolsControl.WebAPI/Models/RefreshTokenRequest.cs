﻿namespace ToolsControl.WebAPI.Models;

public class RefreshTokenRequest
{
    public Guid UserId { get; set; }
    
    public string RefreshToken { get; set; } = string.Empty;
}