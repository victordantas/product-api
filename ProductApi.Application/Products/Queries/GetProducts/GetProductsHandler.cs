using MediatR;
using ProductApi.Application.Common.Dtos;
using ProductApi.Application.Common.Interfaces;

namespace ProductApi.Application.Products.Queries.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _repository;

    public GetProductsHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken ct)
    {
        var products = await _repository.GetAllAsync(ct);

        return products.Select(p => new ProductDto(p.Id, p.Name, p.Price)).ToList();
    }
}