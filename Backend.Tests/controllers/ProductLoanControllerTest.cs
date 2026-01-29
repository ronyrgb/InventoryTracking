using Backend.Controllers;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System;
using System.Threading.Tasks;

namespace Backend.Tests
{
    public class ProductLoanControllerTests
    {
        private readonly Mock<IProductLoanService> _mockService;
        private readonly ProductLoanController _controller;

        public ProductLoanControllerTests()
        {
            _mockService = new Mock<IProductLoanService>();
            _controller = new ProductLoanController(_mockService.Object);
        }

        [Fact]
        public async Task Create_ReturnsCreated_WhenSuccessful()
        {
            var loan = new ProductLoan { Note = "Teste" };

            var result = await _controller.Create(loan);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnLoan = Assert.IsType<ProductLoan>(actionResult.Value);
            Assert.Equal("Teste", returnLoan.Note);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenServiceThrowsKeyNotFound()
        {
            _mockService
                .Setup(s => s.UpdateAsync(It.IsAny<ProductLoan>()))
                .ThrowsAsync(new KeyNotFoundException("Não encontrado"));

            var loan = new ProductLoan
            {
                Id = Guid.NewGuid(),
                Note = "Teste"
            };

            var result = await _controller.Update(loan.Id, loan);

            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

    }
}
