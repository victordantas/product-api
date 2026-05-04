using ProductApi.Application.Products.Commands.CreateProduct;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/products", async (CreateProductCommand cmd, IMediator mediator) =>
{
    var id = await mediator.Send(cmd);
    return Results.Created($"/products/{id}", id);
});

app.Run();