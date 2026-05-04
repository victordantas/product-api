using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;

namespace ProductApi.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}