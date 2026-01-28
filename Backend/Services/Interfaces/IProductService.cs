using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product?> GetByCodeAsync(string code);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
    }
}
