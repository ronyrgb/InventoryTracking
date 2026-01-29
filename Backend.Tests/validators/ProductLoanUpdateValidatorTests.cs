using Backend.Data.DTOs;
using Backend.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Backend.Tests.Validators
{
    public class ProductLoanUpdateValidatorTests
    {
        private readonly ProductLoanUpdateValidator _validator;

        public ProductLoanUpdateValidatorTests()
        {
            _validator = new ProductLoanUpdateValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Note_Too_Long()
        {
            var dto = new ProductLoanUpdateDto
            {
                Note = new string('a', 300) // mais de 250 caracteres
            };

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.Note);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Valid()
        {
            var dto = new ProductLoanUpdateDto
            {
                Note = "Nota válida"
            };

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveValidationErrorFor(x => x.Note);
        }
    }
}
