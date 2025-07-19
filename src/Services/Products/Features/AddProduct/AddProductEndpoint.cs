using System;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Products.Models;

namespace Products.Features.AddProduct;

public record AddProductRequest(string Name, decimal Kalor, decimal Belk, decimal Jir, decimal Uglev);
public record AddProductResponse(Product product);

public class AddProductEndpoint
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/product", async (AddProductRequest request, ISender sender) =>
        {
            var product = request.Adapt<Product>();

            var result = await sender.Send(new AddProductCommand(product));

            return Results.Created("/basket/", result.Adapt<AddProductResponse>());
        })
        .WithName("CreateProduct")
        .Produces<AddProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}
