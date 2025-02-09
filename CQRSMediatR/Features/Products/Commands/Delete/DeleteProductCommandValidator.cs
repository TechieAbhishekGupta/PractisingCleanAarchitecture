using FluentValidation;

namespace CQRSMediatR.Features.Products.Commands.Delete
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Product ID is required.");
        }
    }
}
