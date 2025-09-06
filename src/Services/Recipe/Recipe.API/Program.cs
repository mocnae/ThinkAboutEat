using BuildingBlocks.Exceptions.Handler;
using Carter;
using Recipe.Application;
using Recipe.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration);

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapCarter();

app.UseExceptionHandler(options => {});

app.MapGet("/", () => "Hello World!");

app.Run();
