using CommerceHub.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceHub.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICommerceHubDbContext _context;

        public DeleteCategoryCommandHandler(ICommerceHubDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(new object[] { request.Id }, cancellationToken);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID '{request.Id}' was not found.");
            }

            // Optional: Prevent deletion if products are assigned to this category
            var hasProducts = await _context.Products.AnyAsync(p => p.CategoryId == request.Id, cancellationToken);
            if (hasProducts)
            {
                throw new InvalidOperationException($"Cannot delete category '{category.Name}' because it contains active products.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
