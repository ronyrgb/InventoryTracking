using Backend.Controllers;
using Backend.Data.DTOs;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Backend.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _serviceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _serviceMock = new Mock<IAuthService>();
            _controller = new AuthController(_serviceMock.Object);
        }

        [Fact]
        public async Task Login_ReturnsOk_WhenValid()
        {
            var login = new LoginRequestDto
            {
                Email = "teste@email.com",
                Password = "123"
            };

            var response = new LoginResponseDto
            {
                Token = "jwt-token"
            };

            _serviceMock
                .Setup(x => x.LoginAsync(login))
                .ReturnsAsync(response);

            var result = await _controller.Login(login);

            var ok = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<LoginResponseDto>(ok.Value);

            Assert.Equal("jwt-token", value.Token);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenInvalid()
        {
            var login = new LoginRequestDto
            {
                Email = "teste@email.com",
                Password = "errado"
            };

            _serviceMock
                .Setup(x => x.LoginAsync(login))
                .ReturnsAsync((LoginResponseDto?)null);

            var result = await _controller.Login(login);

            Assert.IsType<UnauthorizedObjectResult>(result.Result);
        }

        [Fact]
        public async Task Login_ReturnsBadRequest_WhenModelInvalid()
        {
            _controller.ModelState.AddModelError("Email", "Obrigatório");

            var result = await _controller.Login(new LoginRequestDto());

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
