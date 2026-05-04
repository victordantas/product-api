namespace ProductApi.Application.Products.Commands.CreateProduct;
using MediatR;

public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;