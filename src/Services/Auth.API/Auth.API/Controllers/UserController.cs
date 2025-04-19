
using Microsoft.AspNetCore.Authorization;

namespace Auth.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null) return NotFound();

        user.FullName = dto.FullName;
        user.Role = dto.Role;
        await _db.SaveChangesAsync();

        return Ok(user);
    }

    public record UpdateUserDto(string FullName, string Role);

}
