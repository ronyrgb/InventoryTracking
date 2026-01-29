using Backend.Data.DTOs;
using Backend.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Backend.Tests.Validators
{
    public class ProductLoanCreateValidatorTests
    {
        private readonly ProductLoanCreateValidator _validator;

        public ProductLoanCreateValidatorTests()
        {
            _validator = new ProductLoanCreateValidator();
        }

        [Fact]
        public void Should_Have_Error_When_ProductId_Is_Empty()
        {
            var dto = new ProductLoanCreateDto
            {
                ProductId = Guid.Empty,
                UserId = Guid.NewGuid(),
                Note = "Teste"
            };

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.ProductId);
        }

        [Fact]
        public void Should_Have_Error_When_UserId_Is_Empty()
        {
            var dto = new ProductLoanCreateDto
            {
                ProductId = Guid.NewGuid(),
                UserId = Guid.Empty,
                Note = "Teste"
            };

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }

        [Fact]
        public void Should_Have_Error_When_Note_Too_Long()
        {
            var dto = new ProductLoanCreateDto
            {
                ProductId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Note = new string('a', 300) // mais de 250 caracteres
            };

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Note);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid()
        {
            var dto = new ProductLoanCreateDto
            {
                ProductId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Note = "Nota válida"
            };

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.ProductId);
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
            result.ShouldNotHaveValidationErrorFor(x => x.Note);
        }
    }
}
