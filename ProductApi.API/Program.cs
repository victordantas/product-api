using ProductApi.Application.Products.Commands.CreateProduct;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.Persistence;
using ProductApi.Application.Products.Queries.GetProducts;
using ProductApi.Application.Products.Queries.GetProductById;
using FluentValidation;
using ProductApi.Application.Products.Commands.UpdateProduct;
using ProductApi.Application.Products.Commands.DeleteProduct;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=products.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API v1");
    options.RoutePrefix = string.Empty;
});

app.MapPost("/products", async (CreateProductCommand cmd, IMediator mediator) =>
{
    try
    {
        var id = await mediator.Send(cmd);
        return Results.Created($"/products/{id}", id);
    }
    catch (ValidationException ex)
    {
        return Results.BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
    }
});


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

app.MapPut("/products/{id:guid}", async (Guid id, UpdateProductCommand cmd, IMediator mediator) =>
{
    if (id != cmd.Id)
        return Results.BadRequest();

    try
    {
        var result = await mediator.Send(cmd);
        return result ? Results.NoContent() : Results.NotFound();
    }
    catch (ValidationException ex)
    {
        return Results.BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
    }
});

app.MapDelete("/products/{id:guid}", async (Guid id, IMediator mediator) =>
{
    try
    {
        var result = await mediator.Send(new DeleteProductCommand(id));
        return result ? Results.NoContent() : Results.NotFound();
    }
    catch (ValidationException ex)
    {
        return Results.BadRequest(new { Errors = ex.Errors.Select(e => e.ErrorMessage) });
    }
});

app.Run();