using MediatR;
using ProductApi.Application.Common.Dtos;

namespace ProductApi.Application.Products.Queries.GetProducts;

public record GetProductsQuery() : IRequest<List<ProductDto>>;