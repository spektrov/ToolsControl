using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ToolsControl.WebAPI.Controllers;

public class BaseApiController : ControllerBase
{
    protected Guid UserId => Guid.Parse(FindClaim(ClaimTypes.NameIdentifier)!);

    protected string Role => FindClaim(ClaimTypes.Role)!;
        
    private string? FindClaim(string claimName)
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
        var claim = claimsIdentity?.FindFirst(claimName);

        return claim?.Value;
    }
}