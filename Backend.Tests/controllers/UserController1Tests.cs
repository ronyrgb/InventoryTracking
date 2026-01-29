using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController1 : ControllerBase
    {
        private readonly IUserService _service;

        public UserController1 (IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(Guid id)
        {
            var user = await _service.GetByIdAsync(id);

            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            try
            {
                await _service.AddAsync(user);

                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Update(Guid id, User user)
        {
            // ✅ PRIMEIRO valida ID
            if (id != user.Id)
                return BadRequest();

            try
            {
                await _service.UpdateAsync(user);
                return Ok(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _service.GetByIdAsync(id);

            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });

            try
            {
                await _service.DeleteAsync(user);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
