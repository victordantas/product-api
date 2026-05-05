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
        app.MapGet("/products", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetProductsQuery());
            return Results.Ok(result);
        });

        app.MapGet("/products/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetProductByIdQuery(id));
            return result is null ? Results.NotFound() : Results.Ok(result);
        });

        app.MapPost("/products", async (CreateProductCommand cmd, IMediator mediator) =>
        {
            var id = await mediator.Send(cmd);
            return Results.Created($"/products/{id}", id);
        });

        app.MapPut("/products/{id:guid}", async (Guid id, UpdateProductCommand cmd, IMediator mediator) =>
        {
            if (id != cmd.Id)
                return Results.BadRequest();

            var result = await mediator.Send(cmd);
            return result ? Results.NoContent() : Results.NotFound();
        });

        app.MapDelete("/products/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteProductCommand(id));
            return result ? Results.NoContent() : Results.NotFound();
        });
    }
}