using ProductApi.Application.Products.Commands.CreateProduct;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.Persistence;
using ProductApi.Application.Products.Queries.GetProducts;
using ProductApi.Application.Products.Queries.GetProductById;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=products.db"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/products", async (CreateProductCommand cmd, IMediator mediator) =>
{
    var id = await mediator.Send(cmd);
    return Results.Created($"/products/{id}", id);
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

app.Run();