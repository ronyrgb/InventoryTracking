
using Backend.Models;
using FluentValidation;

namespace Backend.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.");

            RuleFor(p => p.Code)
                .NotEmpty().WithMessage("O código do produto é obrigatório.");
        }
    }
}
