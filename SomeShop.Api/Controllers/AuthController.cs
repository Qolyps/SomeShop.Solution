using Microsoft.AspNetCore.Mvc;
using SomeShop.Application.Interfaces;
using SomeShop.Application.Services;

namespace SomeShop.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Проверка логина (в реальном проекте будет база данных)
            if (request.Email == "admin@example.com" && request.Password == "password123")
            {
                var token = _jwtTokenService.GenerateToken("1", request.Email);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
