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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // ========================
        // CRUD básico com try/catch
        // ========================
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar todos os usuários: {ex.Message}");
                return new List<User>();
            }
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário por id {id}: {ex.Message}");
                return null;
            }
        }

        public async Task AddAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar usuário: {ex.Message}");
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar usuário {user.Id}: {ex.Message}");
            }
        }

        public async Task DeleteAsync(User user)
        {
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar usuário {user.Id}: {ex.Message}");
            }
        }

        // ========================
        // Buscas específicas
        // ========================
        public async Task<User?> GetByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário por email {email}: {ex.Message}");
                return null;
            }
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário por username {username}: {ex.Message}");
                return null;
            }
        }

        // ========================
        // Refresh token
        // ========================
        public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
        {
            try
            {
                return await _context.Users
                    .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário por refresh token: {ex.Message}");
                return null;
            }
        }

        public async Task UpdateRefreshTokenAsync(User user, string refreshToken, DateTime expiry)
        {
            try
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = expiry;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar refresh token do usuário {user.Id}: {ex.Message}");
            }
        }

        // ========================
        // Consultas adicionais
        // ========================
        public async Task<int> CountAsync()
        {
            try
            {
                return await _context.Users.CountAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao contar usuários: {ex.Message}");
                return 0;
            }
        }

        public async Task<IEnumerable<User>> GetUsersCreatedAfterAsync(DateTime date)
        {
            try
            {
                return await _context.Users.Where(u => u.CreatedAt > date).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuários criados após {date}: {ex.Message}");
                return new List<User>();
            }
        }
    }
}
