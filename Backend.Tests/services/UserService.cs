using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repoMock;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _repoMock = new Mock<IUserRepository>();
            _service = new UserService(_repoMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsUser_WhenUserExists()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "teste@teste.com" };

            _repoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            var result = await _service.GetByIdAsync(userId);

            Assert.Equal(userId, result?.Id);
            Assert.Equal("teste@teste.com", result?.Email);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllUsers()
        {
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Email = "a@a.com" },
                new User { Id = Guid.NewGuid(), Email = "b@b.com" }
            };

            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            var result = await _service.GetAllAsync();

            Assert.Equal(2, result.AsList().Count);
        }

        [Fact]
        public async Task AddAsync_CallsRepository()
        {
            var user = new User { Id = Guid.NewGuid(), Email = "novo@teste.com" };

            await _service.AddAsync(user);

            _repoMock.Verify(r => r.AddAsync(user), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepository()
        {
            var user = new User { Id = Guid.NewGuid(), Email = "update@teste.com" };

            await _service.UpdateAsync(user);

            _repoMock.Verify(r => r.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepository()
        {
            var user = new User { Id = Guid.NewGuid(), Email = "delete@teste.com" };

            await _service.DeleteAsync(user);

            _repoMock.Verify(r => r.DeleteAsync(user), Times.Once);
        }
    }

    public static class EnumerableExtensions
    {
        public static List<T> AsList<T>(this IEnumerable<T> source) => new List<T>(source);
    }
}
