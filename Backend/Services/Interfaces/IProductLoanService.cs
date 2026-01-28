using Backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface IProductLoanService
    {
        Task<IEnumerable<ProductLoan>> GetAllAsync();
        Task<ProductLoan?> GetByIdAsync(Guid id);
        Task AddAsync(ProductLoan loan);
        Task UpdateAsync(ProductLoan loan);
        Task DeleteAsync(ProductLoan loan);

        // Funções de negócio
        Task CheckOutAsync(Guid loanId, Guid userId, string? note = null);
        Task CheckInAsync(Guid loanId);
        Task<IEnumerable<ProductLoan>> GetLoansByProductIdAsync(Guid productId);
        Task<IEnumerable<ProductLoan>> GetActiveLoansAsync();
        Task<IEnumerable<ProductLoan>> GetLoansByUserIdAsync(Guid userId);
    }
}
