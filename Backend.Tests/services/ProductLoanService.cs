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
    public class ProductLoanServiceTests
    {
        private readonly Mock<IProductLoanRepository> _repoMock;
        private readonly ProductLoanService _service;

        public ProductLoanServiceTests()
        {
            _repoMock = new Mock<IProductLoanRepository>();
            _service = new ProductLoanService(_repoMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsLoan()
        {
            var loanId = Guid.NewGuid();
            var loan = new ProductLoan { Id = loanId };

            _repoMock.Setup(r => r.GetByIdAsync(loanId)).ReturnsAsync(loan);

            var result = await _service.GetByIdAsync(loanId);

            Assert.Equal(loanId, result?.Id);
        }

        [Fact]
        public async Task AddAsync_CallsRepository()
        {
            var loan = new ProductLoan { Id = Guid.NewGuid() };

            await _service.AddAsync(loan);

            _repoMock.Verify(r => r.AddAsync(loan), Times.Once);
        }

        [Fact]
        public async Task CheckInAsync_ThrowsIfAlreadyReturned()
        {
            var loan = new ProductLoan { Id = Guid.NewGuid(), ReturnDate = DateTime.UtcNow };

            _repoMock.Setup(r => r.GetByIdAsync(loan.Id)).ReturnsAsync(loan);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CheckInAsync(loan.Id));
        }
    }
}
