using System.Reflection;
using Carter;
using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Features.AddProduct;
using Products.Features.DeleteProduct;
using Products.Features.GetAllProducts;
using Products.Features.GetProduct;
using Products.Features.SearchProduct;
using Products.Features.UpdateProduct;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter(configurator: cfg =>
{
    cfg.WithModule<AddProductEndpoint>();
    cfg.WithModule<UpdateProductEndpoint>();
    cfg.WithModule<GetProductEndpoint>();
    cfg.WithModule<DeleteProductEndpoint>();
    cfg.WithModule<GetAllProductEndpoint>();
    cfg.WithModule<SearchProductEndpoint>();
});

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var connectionString = builder.Configuration.GetConnectionString("database");
builder.Services.AddDbContext<ProductContext>((sp, options) => 
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<ProductRepository>();

var app = builder.Build();

app.UseRouting();

app.MapCarter();

app.Run();
