using Backend.Data.DTOs;
using Backend.Models;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Backend.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly AuthService _service;

        public AuthServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _configMock = new Mock<IConfiguration>();

            // JWT precisa ter NO MÍNIMO 32 bytes
            _configMock.Setup(x => x["Jwt:Key"])
                .Returns("THIS_IS_A_SUPER_SECRET_JWT_KEY_123456789");

            _service = new AuthService(
                _userRepoMock.Object,
                _configMock.Object);
        }

        [Fact]
        public async Task Login_ReturnsToken_WhenValid()
        {
            // Arrange
            var password = "123456";
            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "teste@email.com",
                Username = "teste",
                PasswordHash = hash
            };

            _userRepoMock.Setup(x => x.GetByEmailAsync(user.Email))
                .ReturnsAsync(user);

            var request = new LoginRequestDto
            {
                Email = user.Email,
                Password = password
            };

            // Act
            var result = await _service.LoginAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(result.Token));
            Assert.Equal(user.Username, result.Username);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task Login_ReturnsNull_WhenUserNotFound()
        {
            _userRepoMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            var request = new LoginRequestDto
            {
                Email = "x@x.com",
                Password = "123"
            };

            var result = await _service.LoginAsync(request);

            Assert.Null(result);
        }

        [Fact]
        public async Task Login_ReturnsNull_WhenPasswordInvalid()
        {
            var user = new User
            {
                Email = "x@x.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("outra")
            };

            _userRepoMock.Setup(x => x.GetByEmailAsync(user.Email))
                .ReturnsAsync(user);

            var request = new LoginRequestDto
            {
                Email = user.Email,
                Password = "errada"
            };

            var result = await _service.LoginAsync(request);

            Assert.Null(result);
        }
    }
}
