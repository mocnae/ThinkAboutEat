using System;
using Carter;
using Mapster;
using MediatR;
using Products.Models;

namespace Products.Features.GetAllProducts;

public record GetAllProductsResponse(List<Product> Products);

public class GetAllProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("product/", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAllProductsQuery());

            var response = result.Adapt<GetAllProductsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetAllProducts")
        .Produces<GetAllProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get all products")
        .WithDescription("Get all products");
    }
}
