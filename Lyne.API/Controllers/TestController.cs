using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lyne.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{

    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok(new { message = "This is a public endpoint" });
    }

    [HttpGet("protected")]
    [Authorize]
    public IActionResult Protected()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;

        return Ok(new 
        { 
            message = "This is a protected endpoint",
            userId = userId,
            userEmail = userEmail,
            userName = userName
        });
    }
} 