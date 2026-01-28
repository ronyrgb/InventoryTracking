using Backend.Data.DTOs;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // só admin pode manipular empréstimos
    public class ProductLoanController : ControllerBase
    {
        private readonly IProductLoanService _loanService;
        private readonly ILogger<ProductLoanController> _logger;

        public ProductLoanController(IProductLoanService loanService, ILogger<ProductLoanController> logger)
        {
            _loanService = loanService;
            _logger = logger;
        }

        // GET: api/productloan
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var loans = await _loanService.GetAllAsync();
                return Ok(loans);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os empréstimos");
                return StatusCode(500, new { error = "Erro interno" });
            }
        }

        // GET: api/productloan/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var loan = await _loanService.GetByIdAsync(id);
                if(loan == null)
                    return NotFound(new { error = "Empréstimo não encontrado" });

                return Ok(loan);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar empréstimo ({id})");
                return StatusCode(500, new { error = "Erro interno" });
            }
        }

        // POST: api/productloan
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductLoanCreateDto dto)
        {
            try
            {
                var loan = new ProductLoan
                {
                    ProductId = dto.ProductId,
                    UserId = dto.UserId,
                    Note = dto.Note
                };

                await _loanService.AddAsync(loan);
                return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar empréstimo");
                return StatusCode(500, new { error = "Erro interno" });
            }
        }

        // PUT: api/productloan/checkin/{id}
        [HttpPut("checkin/{id:guid}")]
        public async Task<IActionResult> CheckIn(Guid id)
        {
            try
            {
                await _loanService.CheckInAsync(id);
                return Ok(new { message = "Produto devolvido com sucesso" });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Erro ao fazer check-in ({id})");
                return StatusCode(500, new { error = "Erro interno" });
            }
        }

        // PUT: api/productloan/checkout/{id}
        [HttpPut("checkout/{id:guid}")]
        public async Task<IActionResult> CheckOut(Guid id, [FromQuery] Guid userId, [FromQuery] string? note = null)
        {
            try
            {
                await _loanService.CheckOutAsync(id, userId, note);
                return Ok(new { message = "Produto emprestado com sucesso" });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Erro ao fazer check-out ({id})");
                return StatusCode(500, new { error = "Erro interno" });
            }
        }

        // GET: api/productloan/byproduct/{productId}
        [HttpGet("byproduct/{productId:guid}")]
        public async Task<IActionResult> GetByProductId(Guid productId)
        {
            try
            {
                var loans = await _loanService.GetLoansByProductIdAsync(productId);
                return Ok(loans);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar empréstimos por produto ({productId})");
                return StatusCode(500, new { error = "Erro interno" });
            }
        }

        // GET: api/productloan/byuser/{userId}
        [HttpGet("byuser/{userId:guid}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var loans = await _loanService.GetLoansByUserIdAsync(userId);
                return Ok(loans);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar empréstimos por usuário ({userId})");
                return StatusCode(500, new { error = "Erro interno" });
            }
        }

        // GET: api/productloan/active
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveLoans()
        {
            try
            {
                var loans = await _loanService.GetActiveLoansAsync();
                return Ok(loans);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar empréstimos ativos");
                return StatusCode(500, new { error = "Erro interno" });
            }
        }
    }
}
