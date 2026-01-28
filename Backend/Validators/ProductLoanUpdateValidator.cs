using Backend.Data.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class ProductLoanUpdateValidator : AbstractValidator<ProductLoanUpdateDto>
    {
        public ProductLoanUpdateValidator()
        {
            RuleFor(x => x.Note)
                .MaximumLength(250).WithMessage("Nota muito longa");
        }
    }
}
