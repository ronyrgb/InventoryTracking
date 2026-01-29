using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductLoanController : ControllerBase
    {
        private readonly IProductLoanService _service;

        public ProductLoanController(IProductLoanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductLoan>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductLoan>> GetById(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<IEnumerable<ProductLoan>>> GetByUserId(Guid userId)
        {
            return Ok(await _service.GetLoansByUserIdAsync(userId));
        }

        [HttpGet("product/{productId:guid}")]
        public async Task<ActionResult<IEnumerable<ProductLoan>>> GetByProductId(Guid productId)
        {
            return Ok(await _service.GetLoansByProductIdAsync(productId));
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ProductLoan>>> GetActiveLoans()
        {
            return Ok(await _service.GetActiveLoansAsync());
        }

        [HttpPost]
        public async Task<ActionResult<ProductLoan>> Create([FromBody][Required] ProductLoan loan)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddAsync(loan);

            return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
        }

[HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductLoan>> Update(Guid id, ProductLoan loan)
        {
            if (id != loan.Id)
                return BadRequest();

            try
            {
                await _service.UpdateAsync(loan);
                return Ok(loan);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost("{id:guid}/checkout")]
        public async Task<IActionResult> CheckOut(Guid id, [FromQuery][Required] Guid userId, [FromQuery] string? note = null)
        {
            await _service.CheckOutAsync(id, userId, note);
            return NoContent();
        }

        [HttpPost("{id:guid}/checkin")]
        public async Task<IActionResult> CheckIn(Guid id)
        {
            await _service.CheckInAsync(id);
            return NoContent();
        }
   
    }
}
