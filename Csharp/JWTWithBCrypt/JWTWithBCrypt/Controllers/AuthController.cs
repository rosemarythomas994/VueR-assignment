using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTWithBCrypt.Model;
using JWTWithBCrypt.Service;
using JWTWithBCrypt.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWTWithBCrypt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MongoDbService _mongo;
        private readonly JwtSettings _jwt;

        public AuthController(MongoDbService mongo, IOptions<JwtSettings> jwtOpts)
        {
            _mongo = mongo;
            _jwt = jwtOpts.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel reg)
        {
            if (await _mongo.GetUserAsync(reg.Username) != null)
                return BadRequest("Username exists");

            var hashed = BCrypt.Net.BCrypt.HashPassword(reg.Password);
            await _mongo.AddUserAsync(new User { Username = reg.Username, Password = hashed, Role = reg.Role });
            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _mongo.GetUserAsync(login.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
                return Unauthorized();

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            var user = await _mongo.GetUserAsync(username);
            if (user == null) return NotFound();
            return Ok(new { user.Id, user.Username, user.Role });
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(_jwt.Issuer, _jwt.Audience, claims, expires: DateTime.UtcNow.AddMinutes(_jwt.ExpiryInMinutes), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
