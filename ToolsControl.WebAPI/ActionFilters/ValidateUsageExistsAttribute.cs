using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToolsControl.BLL.Exceptions;
using ToolsControl.BLL.Interfaces;

namespace ToolsControl.WebAPI.ActionFilters;

public class ValidateUsageExistsAttribute : IAsyncActionFilter
{
    private readonly IUsageService _service;
    
    public ValidateUsageExistsAttribute(IUsageService service)
    {
        _service = service;
    }

    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var id = Guid.Parse(context.ActionArguments["id"]?.ToString() ?? string.Empty);
        try
        {
            var model = await _service.GetByIdAsync(id);
            context.HttpContext.Items.Add("usage", model);
            await next();
        }
        catch (ToolsControlException)
        {
            context.Result = new NotFoundResult();
        }
    }
}