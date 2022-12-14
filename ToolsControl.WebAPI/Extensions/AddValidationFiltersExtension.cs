using Microsoft.AspNetCore.Mvc;
using ToolsControl.WebAPI.ActionFilters;

namespace ToolsControl.WebAPI.Extensions;

public static class AddValidationFiltersExtension
{
    public static void AddValidationFilters(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
        
        services.AddScoped<ValidationFilterAttribute>();
        services.AddScoped<ValidateAccessExistsAttribute>();
        services.AddScoped<ValidateEquipmentExistsAttribute>();
        services.AddScoped<ValidateEquipmentTypeExistsAttribute>();
        services.AddScoped<ValidateJobTitleExistsAttribute>();
        services.AddScoped<ValidateUsageExistsAttribute>();
        services.AddScoped<ValidateUserExistsAttribute>();
        services.AddScoped<ValidateWorkerExistsAttribute>();
    }
}