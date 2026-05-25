using ProductApi.Application.Common.Interfaces;
using ProductApi.Application.Products.Commands.CreateProduct;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.Persistence;
using FluentValidation;
using Serilog;
using ProductApi.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddHealthChecks();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

var databasePath = Path.Combine(builder.Environment.ContentRootPath, "products.db");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!
    .Replace("{ContentRootPath}", databasePath);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

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

app.UseMiddleware<ExceptionMiddleware>();
app.UseSerilogRequestLogging();
app.MapHealthChecks("/health");
app.MapProductEndpoints();

app.Run();
