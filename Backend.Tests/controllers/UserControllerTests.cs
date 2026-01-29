using Backend.Controllers;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockService;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockService = new Mock<IUserService>();
            _controller = new UserController(_mockService.Object);
        }

        [Fact]
        public async Task Create_ReturnsCreated_WhenSuccessful()
        {
            var user = new User { Username = "Teste", Email = "teste@email.com" };

            var result = await _controller.Create(user);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnUser = Assert.IsType<User>(actionResult.Value);
            Assert.Equal("Teste", returnUser.Username);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenServiceThrowsKeyNotFound()
        {
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<User>()))
                        .ThrowsAsync(new KeyNotFoundException());

            var user = new User { Username = "Teste", Email = "teste@email.com" };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _controller.Update(Guid.NewGuid(), user));
        }
    }
}
