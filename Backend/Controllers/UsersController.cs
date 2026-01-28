using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST: api/user
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                user.Id = Guid.NewGuid();
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                await _userRepository.AddAsync(user);

                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = "Requisição inválida", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Erro interno no servidor", details = ex.Message });
            }
        }

        // GET: api/user/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { error = "Usuário não encontrado" });

            return Ok(user);
        }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        // PUT: api/user/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] User updatedUser)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { error = "Usuário não encontrado" });

            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;

            if (!string.IsNullOrWhiteSpace(updatedUser.PasswordHash))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updatedUser.PasswordHash);

            await _userRepository.UpdateAsync(user);
            return Ok(user);
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { error = "Usuário não encontrado" });

            await _userRepository.DeleteAsync(user);
            return Ok(new { message = $"Usuário {user.Username} removido com sucesso" });
        }
    }
}
