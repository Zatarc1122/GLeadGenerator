using GLeadGenerator.Contract.Users;
using Microsoft.AspNetCore.Mvc;

namespace GLeadGenerator.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("business")]
    public async Task<ActionResult<IEnumerable<BusinessUserDto>>> GetBusinessUsersAsync()
    {
        return (await _userService.GetBusinessUsersAsync()).ToList();
    }

    [HttpPost]
    public async Task SaveUserAsync([FromBody] UserRequestDto request)
    {
        await _userService.SaveUserAsync(request);
    }
}

