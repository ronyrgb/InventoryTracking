using Backend.Models;
using Backend.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Backend.Tests.Validators
{
    public class UserValidatorTests
    {
        private readonly UserValidator _validator;

        public UserValidatorTests()
        {
            _validator = new UserValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Username_Invalid()
        {
            var user = new User
            {
                Username = "",
                Email = "test@example.com",
                PasswordHash = "123456"
            };

            var result = _validator.TestValidate(user);
            result.ShouldHaveValidationErrorFor(u => u.Username);
        }

        [Fact]
        public void Should_Have_Error_When_Email_Invalid()
        {
            var user = new User
            {
                Username = "Rony",
                Email = "invalid-email",
                PasswordHash = "123456"
            };

            var result = _validator.TestValidate(user);
            result.ShouldHaveValidationErrorFor(u => u.Email);
        }

        [Fact]
        public void Should_Have_Error_When_PasswordHash_Too_Short()
        {
            var user = new User
            {
                Username = "Rony",
                Email = "rony@example.com",
                PasswordHash = "123"
            };

            var result = _validator.TestValidate(user);
            result.ShouldHaveValidationErrorFor(u => u.PasswordHash);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid()
        {
            var user = new User
            {
                Username = "Rony",
                Email = "rony@example.com",
                PasswordHash = "123456"
            };

            var result = _validator.TestValidate(user);
            result.ShouldNotHaveValidationErrorFor(u => u.Username);
            result.ShouldNotHaveValidationErrorFor(u => u.Email);
            result.ShouldNotHaveValidationErrorFor(u => u.PasswordHash);
        }
    }
}
