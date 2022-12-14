namespace ToolsControl.WebAPI.Extensions;

public static class ConfigureControllersExtension
{
    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers(config => 
            { 
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            })
            .AddNewtonsoftJson(
                options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
                })
            .AddXmlDataContractSerializerFormatters();
    }
}