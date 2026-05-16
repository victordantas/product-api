namespace ProductApi.Application.Products.Commands.CreateProduct;

using MediatR;
using Microsoft.Extensions.Logging;
using ProductApi.Application.Common.Interfaces;
using ProductApi.Domain.Entities;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<CreateProductHandler> _logger;

    public CreateProductHandler(IProductRepository repository, ILogger<CreateProductHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken ct)
    {
        _logger.LogInformation(
            "Creating product {ProductName} with price {ProductPrice}",
            request.Name,
            request.Price);

        var product = new Product(request.Name, request.Price);

        await _repository.AddAsync(product, ct);
        await _repository.SaveChangesAsync(ct);

        _logger.LogInformation(
            "Product created successfully with id {ProductId}",
            product.Id);

        return product.Id;
    }
}