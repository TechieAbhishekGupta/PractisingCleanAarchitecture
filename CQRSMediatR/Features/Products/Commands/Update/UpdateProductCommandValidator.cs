using CQRSMediatR.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CQRSMediatR.Features.Products.Commands.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly AppDbContext _context;

        public UpdateProductCommandValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Product ID is required.")
                .MustAsync(ProductExists).WithMessage("Product with the given ID does not exist.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(4).WithMessage("Product name must be at least 4 characters long.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Product description is required.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
        }

        private async Task<bool> ProductExists(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products.AnyAsync(p => p.Id == id, cancellationToken);
        }
    }
}
