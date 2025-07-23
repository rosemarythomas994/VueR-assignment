using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Revv_car_CQRS.Commands;
using Revv_car_CQRS.Model;


namespace Revv_car_CQRS.Controllers
{
   

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IMediator mediator, ILogger<LoginController> logger)
        {
            _mediator = mediator;
            _logger = logger;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommandRequest command)
        {
            var result = await _mediator.Send(command);
            _logger.LogInformation("Registering a new user {@Result}", result);
            if (result.Success) {
                _logger.LogInformation("Successfully registered user with ID: {UserId}", result);
                return Ok(result);
        }
            else
            {
                _logger.LogWarning("User registration failed: {@Result}", result);
                return Conflict(result);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommandRequest request)
        {
            try
            {
                var token = await _mediator.Send(request);
                _logger.LogInformation("Token generated {@Result}", token);

                return Ok(new { token });
            }
            catch (UnauthorizedAccessException)
            {
                _logger.LogWarning("invalid credentials ");

                return Unauthorized("Invalid username or password.");
            }
        }
    }
}
