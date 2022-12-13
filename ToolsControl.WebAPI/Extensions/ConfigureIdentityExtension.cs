using Microsoft.AspNetCore.Identity;
using ToolsControl.DAL;
using ToolsControl.DAL.Entities;
using ToolsControl.WebAPI.Security;


namespace ToolsControl.WebAPI.Extensions;

public static class ConfigureIdentityExtension
{
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        var builder = services.AddIdentityCore<User>(o =>
        {
            o.Password.RequireDigit = true; 
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = true; 
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 8;
            o.User.RequireUniqueEmail = true;
        }); 
        
        builder = new IdentityBuilder(builder.UserType, typeof(ApplicationRole), builder.Services);
        builder.AddEntityFrameworkStores<ToolsDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider)
            .AddTokenProvider("ToolsControlApp", typeof(RefreshTokenProvider<User>));
    }
    
    public static void ConfigureTokens(this IServiceCollection services)
    {
        services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(2));

        services.Configure<RefreshTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromDays(7));
    }
}
