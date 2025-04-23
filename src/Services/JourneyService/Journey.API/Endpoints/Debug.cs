using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Journey.API.Endpoints;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DebugController : ControllerBase
{
    [HttpGet("claims")]
    public IActionResult GetClaims()
    {
        return Ok(User.Claims.Select(c => new { c.Type, c.Value }));
    }
}
