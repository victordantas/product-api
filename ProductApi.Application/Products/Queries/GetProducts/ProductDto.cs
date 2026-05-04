namespace ProductApi.Application.Products.Queries.GetProducts;

public record ProductDto(Guid Id, string Name, decimal Price);