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
            try
            {
                return await _context.ProductLoans
                                     .Include(pl => pl.Product)
                                     .Include(pl => pl.User)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar todos os empréstimos: {ex.Message}");
                return new List<ProductLoan>();
            }
        }

        // =======================
        // Busca um empréstimo pelo ID
        // =======================
        public async Task<ProductLoan?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.ProductLoans
                                     .Include(pl => pl.Product)
                                     .Include(pl => pl.User)
                                     .FirstOrDefaultAsync(pl => pl.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar empréstimo por ID: {ex.Message}");
                return null;
            }
        }

        // =======================
        // Adiciona um novo empréstimo
        // =======================
        public async Task AddAsync(ProductLoan loan)
        {
            try
            {
                await _context.ProductLoans.AddAsync(loan);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar empréstimo: {ex.Message}");
            }
        }

        // =======================
        // Atualiza um empréstimo existente
        // =======================
        public async Task UpdateAsync(ProductLoan loan)
        {
            try
            {
                _context.ProductLoans.Update(loan);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar empréstimo: {ex.Message}");
            }
        }

        // =======================
        // Remove um empréstimo
        // =======================
        public async Task DeleteAsync(ProductLoan loan)
        {
            try
            {
                _context.ProductLoans.Remove(loan);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover empréstimo: {ex.Message}");
            }
        }

        // =======================
        // Empréstimos de um produto específico
        // =======================
        public async Task<IEnumerable<ProductLoan>> GetLoansByProductIdAsync(Guid productId)
        {
            try
            {
                return await _context.ProductLoans
                                     .Include(pl => pl.User)
                                     .Where(pl => pl.ProductId == productId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar empréstimos por produto: {ex.Message}");
                return new List<ProductLoan>();
            }
        }

        // =======================
        // Empréstimos ativos (não devolvidos)
        // =======================
        public async Task<IEnumerable<ProductLoan>> GetActiveLoansAsync()
        {
            try
            {
                return await _context.ProductLoans
                                     .Include(pl => pl.Product)
                                     .Include(pl => pl.User)
                                     .Where(pl => pl.ReturnDate == null)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar empréstimos ativos: {ex.Message}");
                return new List<ProductLoan>();
            }
        }

        // =======================
        // Empréstimos de um usuário específico
        // =======================
        public async Task<IEnumerable<ProductLoan>> GetLoansByUserIdAsync(Guid userId)
        {
            try
            {
                return await _context.ProductLoans
                                     .Include(pl => pl.Product)
                                     .Where(pl => pl.UserId == userId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar empréstimos por usuário: {ex.Message}");
                return new List<ProductLoan>();
            }
        }
    }
}
