using Backend.Data.DTOs;
using FluentValidation;

namespace Backend.Validators
{
    public class ProductLoanCreateValidator : AbstractValidator<ProductLoanCreateDto>
    {
        public ProductLoanCreateValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Produto é obrigatorio");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Usuário é obrigatorio");

            RuleFor(x => x.Note)
                .MaximumLength(250).WithMessage("Nota muito longa");
        }
    }
}
