using FluentValidation;
using SomeShop.Domain.Entities;

namespace SomeShop.Application.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Название продукта не может быть пустым")
                .MinimumLength(3).WithMessage("Название должно быть не менее 3 символов")
                .MaximumLength(100).WithMessage("Название не может превышать 100 символов");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Описание не может быть пустым")
                .MaximumLength(500).WithMessage("Описание не может превышать 500 символов");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Цена должна быть больше 0");
        }
    }
}
