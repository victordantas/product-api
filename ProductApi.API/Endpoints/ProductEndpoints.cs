using MediatR;
using ProductApi.Application.Products.Commands.CreateProduct;
using ProductApi.Application.Products.Commands.DeleteProduct;
using ProductApi.Application.Products.Commands.UpdateProduct;
using ProductApi.Application.Products.Queries.GetProductById;
using ProductApi.Application.Products.Queries.GetProducts;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetProductsQuery(), ct);

            return Results.Ok(result);
        })
        .WithName("GetProducts")
        .WithDescription("Retrieves all products");

        app.MapGet("/products/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetProductByIdQuery(id));
            return result is null ? Results.NotFound() : Results.Ok(result);
        })
        .WithName("GetProductsById")
        .WithDescription("Retrieves a product by its ID");

        app.MapPost("/products", async (CreateProductCommand cmd, IMediator mediator) =>
        {
            var id = await mediator.Send(cmd);
            return Results.Created($"/products/{id}", id);
        })
        .WithName("CreateProduct")
        .WithDescription("Creates a new product");

        app.MapPut("/products/{id:guid}", async (Guid id, UpdateProductCommand cmd, IMediator mediator) =>
        {
            if (id != cmd.Id)
                return Results.BadRequest();

            var result = await mediator.Send(cmd);
            return result ? Results.NoContent() : Results.NotFound();
        })
        .WithName("UpdateProduct")
        .WithDescription("Updates an existing product");

        app.MapDelete("/products/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteProductCommand(id));
            return result ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteProduct")
        .WithDescription("Deletes a product by its ID");
    }
}