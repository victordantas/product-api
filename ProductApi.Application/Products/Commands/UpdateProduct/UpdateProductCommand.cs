namespace ProductApi.Application.Products.Commands.UpdateProduct;
using MediatR;

public record UpdateProductCommand(Guid Id, string Name, decimal Price) : IRequest<bool>;