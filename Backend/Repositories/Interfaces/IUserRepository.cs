using Backend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        // CRUD básico
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);

        // Buscas específicas
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);

        // Consultas adicionais (para relatórios, estatísticas etc.)
        Task<int> CountAsync();
        Task<IEnumerable<User>> GetUsersCreatedAfterAsync(DateTime date);

          
        // Métodos para Refresh Token
    
        Task<User?> GetByRefreshTokenAsync(string refreshToken);
        Task UpdateRefreshTokenAsync(User user, string refreshToken, DateTime expiry);
    }
}
