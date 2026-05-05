namespace ProductApi.Application.Products.Commands.DeleteProduct;
using MediatR;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;