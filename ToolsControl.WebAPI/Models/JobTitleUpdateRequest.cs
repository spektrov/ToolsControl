﻿using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class JobTitleUpdateRequest
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
}