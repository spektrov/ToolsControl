﻿using System.ComponentModel.DataAnnotations;

namespace ToolsControl.WebAPI.Models;

public class EquipmentCreateRequest
{
    [Required]
    public bool IsAvailable { get; set; }
    
    [Required]
    public bool IsAbleToWork { get; set; }
    
    [Required]
    public DateTime LastInspection { get; set; }
    
    public string? Name { get; set; }

    [Required]
    public Guid TypeId { get; set; }
}