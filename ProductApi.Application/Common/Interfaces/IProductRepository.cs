using ProductApi.Domain.Entities;

namespace ProductApi.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken ct);

    Task<List<Product>> GetAllAsync(CancellationToken ct);

    Task AddAsync(Product product, CancellationToken ct);

    void Remove(Product product);

    Task<int> SaveChangesAsync(CancellationToken ct);
}
