using System.Reflection;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Products.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddCarter();

var connectionString = builder.Configuration.GetConnectionString("database");
builder.Services.AddDbContext<ProductContext>((sp, options) => 
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<ProductRepository>();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("database")!);

var app = builder.Build();

app.UseExceptionHandler(options => {});

app.UseRouting();

app.MapCarter();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();
