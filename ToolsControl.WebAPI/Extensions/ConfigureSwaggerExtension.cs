﻿using Microsoft.OpenApi.Models;

namespace ToolsControl.WebAPI.Extensions;

public static class ConfigureSwaggerExtension
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Tools control API",
                Version = "v1",
                Description = "Tools control API by Denys Spektrov. Developed as refactoring labs project.",
                Contact = new OpenApiContact
                {
                    Name = "Denys Spektrov", 
                    Email = "denys.spektrov@nure.ua",
                }
            });

            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            s.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                { 
                    { 
                        new OpenApiSecurityScheme 
                        { 
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, Id = "Bearer"
                            },
                            Name = "Bearer", 
                        }, 
                        new List<string>() 
                    } 
                });
        });
    }
}