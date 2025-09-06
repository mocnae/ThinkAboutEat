using System;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Products.Models;

namespace Products.Features.SearchProduct;

public record SearchProductResponse(List<Products.Models.Product> Products);

public class SearchProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/search/{name}", async (string name, ISender sender) =>
        {
            var result = await sender.Send(new SearchProductQuery(name));

            var response = result.Adapt<SearchProductResponse>();

            return Results.Ok(response);
        })
        .WithName("SearchProducts")
        .Produces<SearchProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Search Products")
        .WithDescription("Search Products");
    }
}
