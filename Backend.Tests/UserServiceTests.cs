using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests
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
        public async Task GetAllAsync_RetornaTodosOsUsuarios()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Email = "user1@test.com" },
                new User { Id = Guid.NewGuid(), Email = "user2@test.com" }
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, ((List<User>)result).Count);
        }

        [Fact]
        public async Task GetByIdAsync_RetornaUsuarioExistente()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Email = "user@test.com" };
            _repoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            var result = await _service.GetByIdAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(userId, result!.Id);
        }

        [Fact]
        public async Task GetByEmailAsync_RetornaUsuarioExistente()
        {
            var email = "user@test.com";
            var user = new User { Id = Guid.NewGuid(), Email = email };
            _repoMock.Setup(r => r.GetByEmailAsync(email)).ReturnsAsync(user);

            var result = await _service.GetByEmailAsync(email);

            Assert.NotNull(result);
            Assert.Equal(email, result!.Email);
        }

        [Fact]
        public async Task AddAsync_ChamaRepositorioAdd()
        {
            var user = new User { Id = Guid.NewGuid(), Email = "new@test.com" };

            await _service.AddAsync(user);

            _repoMock.Verify(r => r.AddAsync(user), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ChamaRepositorioUpdate()
        {
            var user = new User { Id = Guid.NewGuid(), Email = "update@test.com" };

            await _service.UpdateAsync(user);

            _repoMock.Verify(r => r.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ChamaRepositorioDelete()
        {
            var user = new User { Id = Guid.NewGuid(), Email = "delete@test.com" };

            await _service.DeleteAsync(user);

            _repoMock.Verify(r => r.DeleteAsync(user), Times.Once);
        }
    }
}
