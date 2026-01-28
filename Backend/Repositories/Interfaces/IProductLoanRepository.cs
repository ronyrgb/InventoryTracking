using Backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repositories.Interfaces
{
    public interface IProductLoanRepository
    {
        Task<IEnumerable<ProductLoan>> GetAllAsync();
        Task<ProductLoan?> GetByIdAsync(Guid id);
        Task AddAsync(ProductLoan loan);
        Task UpdateAsync(ProductLoan loan);
        Task DeleteAsync(ProductLoan loan);

        // Buscar todos os empréstimos de um produto específico
        Task<IEnumerable<ProductLoan>> GetLoansByProductIdAsync(Guid productId);

        // Buscar empréstimos ativos (em uso)
        Task<IEnumerable<ProductLoan>> GetActiveLoansAsync();

        // Buscar empréstimos de um usuário específico
        Task<IEnumerable<ProductLoan>> GetLoansByUserIdAsync(Guid userId);
    }
}
