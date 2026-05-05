namespace ProductApi.Application.Products.Commands.UpdateProduct;

using MediatR;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Persistence;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly AppDbContext _context;

    public UpdateProductHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken ct)
    {
        var product = await _context.Products.FindAsync(new object[] { request.Id }, ct);

        if (product == null)
            return false;

        product.Update(request.Name, request.Price);

        await _context.SaveChangesAsync(ct);

        return true;
    }
}