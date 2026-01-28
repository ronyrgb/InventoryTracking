using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class ProductLoanService : IProductLoanService
    {
        private readonly IProductLoanRepository _repo;

        public ProductLoanService(IProductLoanRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductLoan>> GetAllAsync()
        {
            try
            {
                return await _repo.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar todos os empréstimos: {ex.Message}");
                throw;
            }
        }

        public async Task<ProductLoan?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _repo.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar empréstimo por ID ({id}): {ex.Message}");
                throw;
            }
        }

        public async Task AddAsync(ProductLoan loan)
        {
            try
            {
                await _repo.AddAsync(loan);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar empréstimo: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAsync(ProductLoan loan)
        {
            try
            {
                await _repo.UpdateAsync(loan);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar empréstimo ({loan.Id}): {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(ProductLoan loan)
        {
            try
            {
                await _repo.DeleteAsync(loan);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar empréstimo ({loan.Id}): {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ProductLoan>> GetLoansByProductIdAsync(Guid productId)
        {
            try
            {
                return await _repo.GetLoansByProductIdAsync(productId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar empréstimos por produto ({productId}): {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ProductLoan>> GetActiveLoansAsync()
        {
            try
            {
                return await _repo.GetActiveLoansAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar empréstimos ativos: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ProductLoan>> GetLoansByUserIdAsync(Guid userId)
        {
            try
            {
                return await _repo.GetLoansByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar empréstimos por usuário ({userId}): {ex.Message}");
                throw;
            }
        }

        public async Task CheckOutAsync(Guid loanId, Guid userId, string? note = null)
        {
            try
            {
                var loan = await _repo.GetByIdAsync(loanId);
                if (loan == null)
                    throw new InvalidOperationException("Empréstimo não encontrado.");

                if (loan.ReturnDate == null)
                    throw new InvalidOperationException("Produto ainda está emprestado.");

                loan.CheckOut(userId, note);
                await _repo.UpdateAsync(loan);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer check-out do empréstimo ({loanId}): {ex.Message}");
                throw;
            }
        }

        public async Task CheckInAsync(Guid loanId)
        {
            try
            {
                var loan = await _repo.GetByIdAsync(loanId);
                if (loan == null)
                    throw new InvalidOperationException("Empréstimo não encontrado.");

                if (loan.ReturnDate != null)
                    throw new InvalidOperationException("Produto já devolvido.");

                loan.CheckIn();
                await _repo.UpdateAsync(loan);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer check-in do empréstimo ({loanId}): {ex.Message}");
                throw;
            }
        }
    }
}
