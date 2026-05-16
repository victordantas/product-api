namespace ProductApi.Application.Products.Commands.DeleteProduct;

using MediatR;
using Microsoft.Extensions.Logging;
using ProductApi.Application.Common.Interfaces;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repository;
    private readonly ILogger<DeleteProductHandler> _logger;
    
    public DeleteProductHandler(IProductRepository repository, ILogger<DeleteProductHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken ct)
    {
        var product = await _repository.GetByIdAsync(request.Id, ct);

        if (product == null)
        {
            _logger.LogWarning(
                "Product {ProductId} not found for deletion",
                request.Id);
            return false;
        }

        _repository.Remove(product);
        await _repository.SaveChangesAsync(ct);

        return true;
    }
}