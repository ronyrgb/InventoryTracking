using Backend.Data;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Repositories
{
    public class ProductLoanRepository : IProductLoanRepository
    {
        private readonly AppDbContext _context;

        public ProductLoanRepository(AppDbContext context)
        {
            _context = context;
        }

        // =======================
        // Retorna todos os empréstimos
        // =======================
        public async Task<IEnumerable<ProductLoan>> GetAllAsync()
        {
            return await _context.ProductLoans
                                 .Include(pl => pl.Product)
                                 .Include(pl => pl.User)
                                 .ToListAsync();
        }

        // =======================
        // Busca um empréstimo pelo ID
        // =======================
        public async Task<ProductLoan?> GetByIdAsync(Guid id)
        {
            return await _context.ProductLoans
                                 .Include(pl => pl.Product)
                                 .Include(pl => pl.User)
                                 .FirstOrDefaultAsync(pl => pl.Id == id);
        }

        // =======================
        // Adiciona um novo empréstimo
        // =======================
        public async Task AddAsync(ProductLoan loan)
        {
            await _context.ProductLoans.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        // =======================
        // Atualiza um empréstimo existente
        // =======================
        public async Task UpdateAsync(ProductLoan loan)
        {
            _context.ProductLoans.Update(loan);
            await _context.SaveChangesAsync();
        }

        // =======================
        // Remove um empréstimo
        // =======================
        public async Task DeleteAsync(ProductLoan loan)
        {
            _context.ProductLoans.Remove(loan);
            await _context.SaveChangesAsync();
        }

        // =======================
        // Empréstimos de um produto específico
        // =======================
        public async Task<IEnumerable<ProductLoan>> GetLoansByProductIdAsync(Guid productId)
        {
            return await _context.ProductLoans
                                 .Include(pl => pl.User)
                                 .Where(pl => pl.ProductId == productId)
                                 .ToListAsync();
        }

        // =======================
        // Empréstimos ativos (não devolvidos)
        // =======================
        public async Task<IEnumerable<ProductLoan>> GetActiveLoansAsync()
        {
            return await _context.ProductLoans
                                 .Include(pl => pl.Product)
                                 .Include(pl => pl.User)
                                 .Where(pl => pl.ReturnDate == null)
                                 .ToListAsync();
        }

        // =======================
        // Empréstimos de um usuário específico
        // =======================
        public async Task<IEnumerable<ProductLoan>> GetLoansByUserIdAsync(Guid userId)
        {
            return await _context.ProductLoans
                                 .Include(pl => pl.Product)
                                 .Where(pl => pl.UserId == userId)
                                 .ToListAsync();
        }
    }
}
