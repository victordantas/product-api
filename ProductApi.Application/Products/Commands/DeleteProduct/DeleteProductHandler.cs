namespace ProductApi.Application.Products.Commands.DeleteProduct;

using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Persistence;
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly AppDbContext _context;

    public DeleteProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken ct)
    {
        var product = await _context.Products.FindAsync(new object[] { request.Id }, ct);

        if (product == null)
            return false;

        _context.Products.Remove(product);

        await _context.SaveChangesAsync(ct);

        return true;
    }
}