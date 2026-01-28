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
        public async Task GetAllAsync_RetornaTodosOsEmprestimos()
        {
            var loans = new List<ProductLoan>
            {
                new ProductLoan { Id = Guid.NewGuid() },
                new ProductLoan { Id = Guid.NewGuid() }
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(loans);

            var result = await _service.GetAllAsync();

            Assert.Equal(2, ((List<ProductLoan>)result).Count);
        }

        [Fact]
        public async Task GetByIdAsync_RetornaEmprestimoExistente()
        {
            var loanId = Guid.NewGuid();
            var loan = new ProductLoan { Id = loanId };
            _repoMock.Setup(r => r.GetByIdAsync(loanId)).ReturnsAsync(loan);

            var result = await _service.GetByIdAsync(loanId);

            Assert.NotNull(result);
            Assert.Equal(loanId, result!.Id);
        }

        [Fact]
        public async Task AddAsync_ChamaRepositorioAdd()
        {
            var loan = new ProductLoan { Id = Guid.NewGuid() };

            await _service.AddAsync(loan);

            _repoMock.Verify(r => r.AddAsync(loan), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ChamaRepositorioUpdate()
        {
            var loan = new ProductLoan { Id = Guid.NewGuid() };

            await _service.UpdateAsync(loan);

            _repoMock.Verify(r => r.UpdateAsync(loan), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ChamaRepositorioDelete()
        {
            var loan = new ProductLoan { Id = Guid.NewGuid() };

            await _service.DeleteAsync(loan);

            _repoMock.Verify(r => r.DeleteAsync(loan), Times.Once);
        }

        [Fact]
        public async Task CheckOutAsync_DeveAtualizarQuandoProdutoDisponivel()
        {
            var loanId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var loan = new ProductLoan
            {
                Id = loanId,
                ReturnDate = DateTime.UtcNow // produto disponível
            };

            _repoMock.Setup(r => r.GetByIdAsync(loanId)).ReturnsAsync(loan);

            await _service.CheckOutAsync(loanId, userId, "Nota teste");

            _repoMock.Verify(r => r.UpdateAsync(It.Is<ProductLoan>(l => l.Id == loanId)), Times.Once);
            Assert.Equal(userId, loan.UserId); // assumindo que CheckOut atualiza UserId
        }

        [Fact]
        public async Task CheckOutAsync_DeveLancarQuandoProdutoIndisponivel()
        {
            var loanId = Guid.NewGuid();
            var loan = new ProductLoan { Id = loanId, ReturnDate = null }; // já emprestado
            _repoMock.Setup(r => r.GetByIdAsync(loanId)).ReturnsAsync(loan);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CheckOutAsync(loanId, Guid.NewGuid()));
        }

        [Fact]
        public async Task CheckInAsync_DeveAtualizarQuandoProdutoDevolvido()
        {
            var loanId = Guid.NewGuid();
            var loan = new ProductLoan { Id = loanId, ReturnDate = null };
            _repoMock.Setup(r => r.GetByIdAsync(loanId)).ReturnsAsync(loan);

            await _service.CheckInAsync(loanId);

            _repoMock.Verify(r => r.UpdateAsync(It.Is<ProductLoan>(l => l.Id == loanId)), Times.Once);
            Assert.NotNull(loan.ReturnDate);
        }

        [Fact]
        public async Task CheckInAsync_DeveLancarQuandoProdutoJaDevolvido()
        {
            var loanId = Guid.NewGuid();
            var loan = new ProductLoan { Id = loanId, ReturnDate = DateTime.UtcNow };
            _repoMock.Setup(r => r.GetByIdAsync(loanId)).ReturnsAsync(loan);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CheckInAsync(loanId));
        }
    }
}
