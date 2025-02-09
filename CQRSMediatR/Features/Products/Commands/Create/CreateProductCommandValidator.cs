using FluentValidation;

namespace CQRSMediatR.Features.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(4).WithMessage("Product name must be at least 4 characters long.")
                .MaximumLength(50).WithMessage("Product name must not exceed 50 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Product description is required.")
                .MinimumLength(10).WithMessage("Product description must be at least 10 characters long.")
                .MaximumLength(100).WithMessage("Product description must not exceed 100 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.")
                .LessThanOrEqualTo(10000).WithMessage("Price must not exceed 10,000.");
        }
    }
}
