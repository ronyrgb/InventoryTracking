using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ProductLoanService : IProductLoanService
    {
        private readonly IProductLoanRepository _repo;

        public ProductLoanService(IProductLoanRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductLoan>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<ProductLoan?> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(ProductLoan loan)
        {
            await _repo.AddAsync(loan);
        }

        public async Task UpdateAsync(ProductLoan loan)
        {
            await _repo.UpdateAsync(loan);
        }

        public async Task DeleteAsync(ProductLoan loan)
        {
            await _repo.DeleteAsync(loan);
        }

        public async Task<IEnumerable<ProductLoan>> GetLoansByProductIdAsync(Guid productId)
        {
            return await _repo.GetLoansByProductIdAsync(productId);
        }

        public async Task<IEnumerable<ProductLoan>> GetActiveLoansAsync()
        {
            return await _repo.GetActiveLoansAsync();
        }

        public async Task<IEnumerable<ProductLoan>> GetLoansByUserIdAsync(Guid userId)
        {
            return await _repo.GetLoansByUserIdAsync(userId);
        }

        public async Task CheckOutAsync(Guid loanId, Guid userId, string? note = null)
        {
            var loan = await _repo.GetByIdAsync(loanId);
            if (loan == null)
                throw new InvalidOperationException("Empréstimo não encontrado.");

            if (loan.ReturnDate == null)
                throw new InvalidOperationException("Produto ainda está emprestado.");

            loan.CheckOut(userId, note);
            await _repo.UpdateAsync(loan);
        }

        public async Task CheckInAsync(Guid loanId)
        {
            var loan = await _repo.GetByIdAsync(loanId);
            if (loan == null)
                throw new InvalidOperationException("Empréstimo não encontrado.");

            if (loan.ReturnDate != null)
                throw new InvalidOperationException("Produto já devolvido.");

            loan.CheckIn();
            await _repo.UpdateAsync(loan);
        }
    }
}
