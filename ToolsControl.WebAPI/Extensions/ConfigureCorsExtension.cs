namespace ToolsControl.WebAPI.Extensions;

public static class ConfigureCorsExtension
{
    public static void ConfigureCors(this IServiceCollection services) => 
        services.AddCors(options => { options.AddPolicy("CorsPolicy", builder => 
            builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("x-pagination")); 
        });
}