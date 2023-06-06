﻿using MarketPlace.Helpers;
using MarketplaceBackend.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JWTSettings _options;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizeController(UserManager<IdentityUser> user,
            SignInManager<IdentityUser> signIn,
            IOptions<JWTSettings> optAccess,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = user;
            _signInManager = signIn;
            _options = optAccess.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO paramUser)
        {
            var user = new IdentityUser { UserName = paramUser.UserName, Email = paramUser.Email };

            var result = await _userManager.CreateAsync(user, paramUser.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Email, paramUser.Email));

                await _userManager.AddClaimsAsync(user, claims);
            }
            else
            {
                return BadRequest();
            }
            return Ok();
        }

        private string GetToken(IdentityUser user, IEnumerable<Claim> principal)
        {
            var claims = principal.ToList();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserDTO paramUser)
        {
            var user = await _userManager.FindByEmailAsync(paramUser.Email);

            if (user == null)
                return BadRequest("Error");

            var result = await _signInManager.PasswordSignInAsync(user, paramUser.Password, false, false);

            if (result.Succeeded)
            {
                IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);
                var token = GetToken(user, claims);

                return Ok(token);
            }

            return BadRequest("Error");
        }

        [HttpGet("CurrentUser")]
        [Authorize]
        public IActionResult GetCurUser()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(jwtToken.Claims);
            var user = new ClaimsPrincipal(identity);

            return Ok(user);
        }

        [HttpGet("CurUserId")]
        [Authorize]
        public async Task<string> GetUserId(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user.Id;
        }
    }
}
