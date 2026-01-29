using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _service.GetAllAsync();
            return Ok(users);
        }

        /// <summary>
        /// Retorna um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> GetById(Guid id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { error = "Usuário não encontrado" });

            return Ok(user);
        }

        /// <summary>
        /// Retorna um usuário pelo e-mail.
        /// </summary>
        /// <param name="email">E-mail do usuário.</param>
        [HttpGet("email/{email}")]
        public async Task<ActionResult<User>> GetByEmail([EmailAddress] string email)
        {
            var user = await _service.GetByEmailAsync(email);
            if (user == null)
                return NotFound(new { error = "Usuário não encontrado" });

            return Ok(user);
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody][Required] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<User>> Update(Guid id, [FromBody][Required] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            user.Id = id;
            await _service.UpdateAsync(user);
            return Ok(user);
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { error = "Usuário não encontrado" });

            await _service.DeleteAsync(user);
            return NoContent();
        }
    }
}
