using Backend.Data.DTOs;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto LoginRequestValidator)
        {
            var result = await _authService.LoginAsync(LoginRequestValidator);

            if (result == null)
                return Unauthorized(new { error = "Email ou senha incorretos" });

            return Ok(result);
        }
    }
}

