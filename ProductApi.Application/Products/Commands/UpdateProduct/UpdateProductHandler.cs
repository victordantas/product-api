namespace ProductApi.Application.Products.Commands.UpdateProduct;

using MediatR;
using Microsoft.Extensions.Logging;
using ProductApi.Application.Common.Interfaces;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<UpdateProductHandler> _logger;

    public UpdateProductHandler(IProductRepository repository, ILogger<UpdateProductHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken ct)
    {
        _logger.LogInformation(
            "Updating product {ProductName}",
            request.Name);

        var product = await _repository.GetByIdAsync(request.Id, ct);

        if (product == null)
            return false;

        product.Update(request.Name, request.Price);

        await _repository.SaveChangesAsync(ct);

        return true;
    }
}