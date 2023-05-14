using MarketPlace.Models;
using MarketplaceBackend.Services;
using MarketplaceBackend.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MarketplaceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("FindByEmail")]
        public User FindUserByEmail([FromQuery] string email)
        {
            var user = _userService.FindUserByEmail(email);
            return user;
        }
    }
}
