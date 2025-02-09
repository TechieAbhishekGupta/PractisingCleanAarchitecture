using CQRSMediatR.Persistence;
using MediatR;

namespace CQRSMediatR.Features.Products.Commands.Update
{
    public class UpdateProductCommandHandler(AppDbContext context) : IRequestHandler<UpdateProductCommand>
    {
        public async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await context.Products.FindAsync(command.Id);
            if (product == null) return;

            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;

            await context.SaveChangesAsync();
        }
    }
}
