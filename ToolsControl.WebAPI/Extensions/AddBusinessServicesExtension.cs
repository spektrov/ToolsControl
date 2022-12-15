using ToolsControl.BLL.Interfaces;
using ToolsControl.BLL.Services;

namespace ToolsControl.WebAPI.Extensions;

public static class AddBusinessServicesExtension
{
    public static void AddAddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<IAccessService, AccessService>();
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<IEquipmentTypeService, EquipmentTypeService>();
        services.AddScoped<IJobTitleService, JobTitleService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUsageService, UsageService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWorkerService, WorkerService>();
        services.AddScoped<IStatisticService, StatisticService>();
    }
}