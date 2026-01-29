using Backend.Controllers;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Backend.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _serviceMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _serviceMock = new Mock<IProductService>();
            _controller = new ProductController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk()
        {
            _serviceMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Product>());

            var result = await _controller.GetAll();

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenNull()
        {
            _serviceMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Product?)null);

            var result = await _controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreated()
        {
            var product = new Product { Id = Guid.NewGuid(), Code = "ABC" };

            _serviceMock.Setup(x => x.AddAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Create(product);

            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task Update_ReturnsOk()
        {
            var product = new Product { Id = Guid.NewGuid(), Code = "ABC" };

            _serviceMock.Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            var result = await _controller.Update(product.Id, product);

            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            var product = new Product { Id = Guid.NewGuid() };

            var result = await _controller.Update(Guid.NewGuid(), product);

            Assert.IsType<BadRequestResult>(result.Result);
        }
    }
}
