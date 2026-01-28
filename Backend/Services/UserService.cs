using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _repo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário por ID ({id}): {ex.Message}");
                throw;
            }
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            try
            {
                return await _repo.GetByEmailAsync(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário pelo email ({email}): {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await _repo.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar todos os usuários: {ex.Message}");
                throw;
            }
        }

        public async Task AddAsync(User user)
        {
            try
            {
                await _repo.AddAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar usuário ({user.Email}): {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                await _repo.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar usuário ({user.Email}): {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(User user)
        {
            try
            {
                await _repo.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar usuário ({user.Email}): {ex.Message}");
                throw;
            }
        }
    }
}
