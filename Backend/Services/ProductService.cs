using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _repo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar produto por ID ({id}): {ex.Message}");
                throw;
            }
        }

        public async Task<Product?> GetByCodeAsync(string code)
        {
            try
            {
                return await _repo.GetByCodeAsync(code);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar produto pelo código ({code}): {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await _repo.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar todos os produtos: {ex.Message}");
                throw;
            }
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                await _repo.AddAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar produto ({product.Code}): {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                await _repo.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar produto ({product.Code}): {ex.Message}");
                throw;
            }
        }
    }
}
