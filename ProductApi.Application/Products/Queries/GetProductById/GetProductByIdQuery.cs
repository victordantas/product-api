using MediatR;
using ProductApi.Application.Common.Dtos;

namespace ProductApi.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;