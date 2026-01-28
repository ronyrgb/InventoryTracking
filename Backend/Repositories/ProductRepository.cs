using Backend.Data;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Backend.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // =======================
        // Retorna todos os produtos
        // =======================
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar todos os produtos: {ex.Message}");
                return new List<Product>();
            }
        }

        // =======================
        // Busca um produto pelo ID
        // =======================
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Products.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar produto por ID: {ex.Message}");
                return null;
            }
        }

        // =======================
        // Busca um produto pelo código
        // =======================
        public async Task<Product?> GetByCodeAsync(string code)
        {
            try
            {
                return await _context.Products.FirstOrDefaultAsync(p => p.Code == code);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar produto por código: {ex.Message}");
                return null;
            }
        }

        // =======================
        // Adiciona um novo produto
        // =======================
        public async Task AddAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar produto: {ex.Message}");
            }
        }

        // =======================
        // Atualiza um produto existente
        // =======================
        public async Task UpdateAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
            }
        }

        // =======================
        // Remove um produto
        // =======================
        public async Task DeleteAsync(Product product)
        {
            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover produto: {ex.Message}");
            }
        }
    }
}
