namespace ProductApi.Application.Products.Commands.CreateProduct;

using MediatR;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken ct)
    {
        return Guid.NewGuid();
    }
}