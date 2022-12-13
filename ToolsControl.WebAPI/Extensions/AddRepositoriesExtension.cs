using ToolsControl.DAL.Interfaces;
using ToolsControl.DAL.Repositories;

namespace ToolsControl.WebAPI.Extensions;

public static class AddRepositoriesExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAllowedAccessRepository, AllowedAccessRepository>();
        services.AddScoped<IEquipmentRepository, EquipmentRepository>();
        services.AddScoped<IEquipmentTypeRepository, EquipmentTypeRepository>();
        services.AddScoped<IJobTitleRepository, JobTitleRepository>();
        services.AddScoped<IUsageRepository, UsageRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkerRepository, WorkerRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
    }
}