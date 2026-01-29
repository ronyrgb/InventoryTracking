using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests.Services
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
        public async Task GetByIdAsync_ReturnsProduct()
        {
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Code = "P001" };

            _repoMock.Setup(r => r.GetByIdAsync(productId)).ReturnsAsync(product);

            var result = await _service.GetByIdAsync(productId);

            Assert.Equal(productId, result?.Id);
            Assert.Equal("P001", result?.Code);
        }

        [Fact]
        public async Task GetByCodeAsync_ReturnsProduct()
        {
            var code = "P001";
            var product = new Product { Id = Guid.NewGuid(), Code = code };

            _repoMock.Setup(r => r.GetByCodeAsync(code)).ReturnsAsync(product);

            var result = await _service.GetByCodeAsync(code);

            Assert.Equal(code, result?.Code);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Code = "A" },
                new Product { Id = Guid.NewGuid(), Code = "B" }
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            var result = await _service.GetAllAsync();

            Assert.Equal(2, result.AsList().Count);
        }

        [Fact]
        public async Task AddAsync_CallsRepository()
        {
            var product = new Product { Id = Guid.NewGuid(), Code = "NEW" };

            await _service.AddAsync(product);

            _repoMock.Verify(r => r.AddAsync(product), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepository()
        {
            var product = new Product { Id = Guid.NewGuid(), Code = "UPD" };

            await _service.UpdateAsync(product);

            _repoMock.Verify(r => r.UpdateAsync(product), Times.Once);
        }
    }
}
