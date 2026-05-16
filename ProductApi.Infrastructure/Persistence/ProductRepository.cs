using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Common.Interfaces;
using ProductApi.Domain.Entities;

namespace ProductApi.Infrastructure.Persistence;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.Products.FindAsync(new object[] { id }, ct);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task AddAsync(Product product, CancellationToken ct)
    {
        await _context.Products.AddAsync(product, ct);
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }

    public Task<int> SaveChangesAsync(CancellationToken ct)
    {
        return _context.SaveChangesAsync(ct);
    }
}
