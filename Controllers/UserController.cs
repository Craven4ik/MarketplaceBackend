using MarketPlace.Models;
using MarketplaceBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("FindByEmail")]
    public User FindUserByEmail([FromQuery] string email)
        => _userService.FindUserByEmail(email);

    [HttpGet("FindUserIdByEmail")]
    public string FindUserIdByEmail([FromQuery] string email)
        => _userService.FindUserIdByEmail(email);

    [HttpGet("FindUserById")]
    public User FindUserById([FromQuery] string id)
        => _userService.FindUserById(id);
}
