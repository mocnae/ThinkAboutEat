using System;
using Carter;
using Mapster;
using MediatR;

namespace Products.Features.GetProduct;

public record GetProductResponse(string Name, decimal Kalor, decimal Belk, decimal Jir, decimal Uglev);

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/{name}", async (string name, ISender sender) =>
        {
            var result = await sender.Send(new GetProductQuery(name));

            var response = result.Product.Adapt<GetProductResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProduct")
        .Produces<GetProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product")
        .WithDescription("Get Product");
    }
}
