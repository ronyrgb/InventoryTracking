using Backend.Models;
using Backend.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Backend.Tests.Validators
{
    public class ProductValidatorTests
    {
        private readonly ProductValidator _validator;

        public ProductValidatorTests()
        {
            _validator = new ProductValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var product = new Product
            {
                Name = "",
                Code = "ABC123"
            };

            var result = _validator.TestValidate(product);
            result.ShouldHaveValidationErrorFor(p => p.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Code_Is_Empty()
        {
            var product = new Product
            {
                Name = "Produto",
                Code = ""
            };

            var result = _validator.TestValidate(product);
            result.ShouldHaveValidationErrorFor(p => p.Code);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid()
        {
            var product = new Product
            {
                Name = "Produto",
                Code = "ABC123"
            };

            var result = _validator.TestValidate(product);
            result.ShouldNotHaveValidationErrorFor(p => p.Name);
            result.ShouldNotHaveValidationErrorFor(p => p.Code);
        }
    }
}
