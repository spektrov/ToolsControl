using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace ToolsControl.WebAPI.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
                context.Response.ContentType = "application/json"; 
                
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode, 
                        Message = "Internal Server Error."
                    }.ToString());
                }
            });
        });
    }
}

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    
    public override string ToString() => JsonSerializer.Serialize(this);
}