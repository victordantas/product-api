using MediatR;
using ProductApi.Application.Common.Dtos;
using ProductApi.Application.Common.Interfaces;

namespace ProductApi.Application.Products.Queries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _repository;

    public GetProductByIdHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken ct)
    {
        var product = await _repository.GetByIdAsync(request.Id, ct);

        if (product == null) return null;

        return new ProductDto(product.Id, product.Name, product.Price);
    }
}