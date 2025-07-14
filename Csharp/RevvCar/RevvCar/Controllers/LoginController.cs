using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Revv_Cars.Model;
using RevvCar.Models;
using RevvCar.Services;

namespace RevvCar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            var existingUser = await _loginService.AuthenticateAsync(user.Username, user.Password);
            if (existingUser != null)
            {
                return BadRequest(new { message = "User already exists." });
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var usersCollection = HttpContext.RequestServices.GetService<IMongoClient>()
                .GetDatabase("cars")
                .GetCollection<User>("users");
            await usersCollection.InsertOneAsync(user);
            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            var user = await _loginService.AuthenticateAsync(loginModel.Username, loginModel.Password);
            if (user != null)
            {
                var token = _loginService.GenerateJwtToken(user);
                return Ok(new { token, username = user.Username });
            }
            return Unauthorized(new { message = "Invalid email or password" });
        }
    }
}