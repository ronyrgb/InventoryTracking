using Backend.Data.DTOs;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    /// <summary>
    /// Controller responsável pela autenticação de usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Realiza login de um usuário.
        /// </summary>
        /// <param name="loginRequest">Objeto contendo email e senha do usuário.</param>
        /// <returns>Token de autenticação e informações do usuário.</returns>
        /// <response code="200">Login realizado com sucesso.</response>
        /// <response code="400">Campos obrigatórios não foram preenchidos.</response>
        /// <response code="401">Email ou senha incorretos.</response>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(loginRequest);

            if (result == null)
                return Unauthorized(new { error = "Email ou senha incorretos" });

            return Ok(result);
        }
    }
}
