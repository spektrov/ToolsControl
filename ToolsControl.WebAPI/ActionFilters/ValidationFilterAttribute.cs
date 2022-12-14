using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToolsControl.WebAPI.ActionFilters;

public class ValidationFilterAttribute : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.RouteData.Values["action"]; 
        var controller = context.RouteData.Values["controller"]; 
        var param = context.ActionArguments
            .SingleOrDefault(x => x.Value != null
                                  && (x.Value.ToString()!.Contains("Model") 
                                      || x.Value.ToString()!.Contains("Request"))).Value;

        if (param == null)
        {
            context.Result = new BadRequestObjectResult(
                $"Object is null. Controller: {controller}, action: {action}"); 
            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }


    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Not supported.
    }
}