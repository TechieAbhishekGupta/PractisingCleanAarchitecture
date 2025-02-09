using FluentValidation;

namespace CQRSMediatR.Features.Products.Queries.Get
{
    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("Product ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid product ID.");
        }
    }
}
