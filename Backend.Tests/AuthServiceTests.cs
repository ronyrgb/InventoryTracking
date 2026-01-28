using Backend.DTOs;
using Backend.Repositories.Interfaces;
using Backend.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Backend.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();

            // Chave JWT longa suficiente para HMAC-SHA256 (mínimo 32 caracteres)
            _configurationMock.Setup(c => c["Jwt:Key"])
                .Returns("MinhaChaveSuperSecretaMuitoSegura1234"); // 36 caracteres

            _authService = new AuthService(_userRepositoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ComCredenciaisValidas_RetornaToken()
        {
            // Arrange
            var email = "teste@email.com";
            var senha = "senha123";
            var hashSenha = BCrypt.Net.BCrypt.HashPassword(senha);

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(email))
                .ReturnsAsync(new Backend.Models.User
                {
                    Id = Guid.NewGuid(),
                    Username = "UsuarioTeste",
                    Email = email,
                    PasswordHash = hashSenha
                });

            var request = new LoginRequestDto
            {
                Email = email,
                Password = senha
            };

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result!.Email);
            Assert.NotNull(result.Token);
        }

        [Fact]
        public async Task LoginAsync_ComEmailInvalido_RetornaNulo()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.GetByEmailAsync("invalido@email.com"))
                .ReturnsAsync((Backend.Models.User?)null);

            var request = new LoginRequestDto
            {
                Email = "invalido@email.com",
                Password = "qualquer"
            };

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_ComSenhaInvalida_RetornaNulo()
        {
            // Arrange
            var email = "teste@email.com";
            var hashSenha = BCrypt.Net.BCrypt.HashPassword("senhaCorreta");

            _userRepositoryMock.Setup(r => r.GetByEmailAsync(email))
                .ReturnsAsync(new Backend.Models.User
                {
                    Id = Guid.NewGuid(),
                    Username = "UsuarioTeste",
                    Email = email,
                    PasswordHash = hashSenha
                });

            var request = new LoginRequestDto
            {
                Email = email,
                Password = "senhaErrada"
            };

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.Null(result);
        }
    }
}
