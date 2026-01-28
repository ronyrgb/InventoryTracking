using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repoMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repoMock = new Mock<IProductRepository>();
            _service = new ProductService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_RetornaTodosOsProdutos()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Code = "P001" },
                new Product { Id = Guid.NewGuid(), Code = "P002" }
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, ((List<Product>)result).Count);
        }

        [Fact]
        public async Task GetByIdAsync_RetornaProdutoExistente()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Code = "P001" };
            _repoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _service.GetByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result!.Id);
        }

        [Fact]
        public async Task GetByCodeAsync_RetornaProdutoExistente()
        {
            // Arrange
            var code = "P001";
            var product = new Product { Id = Guid.NewGuid(), Code = code };
            _repoMock.Setup(r => r.GetByCodeAsync(code)).ReturnsAsync(product);

            // Act
            var result = await _service.GetByCodeAsync(code);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(code, result!.Code);
        }

        [Fact]
        public async Task AddAsync_ChamaRepositorioAdd()
        {
            // Arrange
            var product = new Product { Id = Guid.NewGuid(), Code = "P003" };

            // Act
            await _service.AddAsync(product);

            // Assert
            _repoMock.Verify(r => r.AddAsync(product), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ChamaRepositorioUpdate()
        {
            // Arrange
            var product = new Product { Id = Guid.NewGuid(), Code = "P004" };

            // Act
            await _service.UpdateAsync(product);

            // Assert
            _repoMock.Verify(r => r.UpdateAsync(product), Times.Once);
        }
    }
}
