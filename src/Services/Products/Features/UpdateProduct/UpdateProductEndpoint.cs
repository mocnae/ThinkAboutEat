using System;
using Carter;
using Mapster;
using MediatR;
using Products.Models;

namespace Products.Features.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, decimal Kalor, decimal Belk, decimal Jir, decimal Uglev);
public record UpdateProductResponse(Product product);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/product", async (UpdateProductRequest request, ISender sender) =>
        {
            var product = request.Adapt<Product>();

            var result = await sender.Send(new UpdateProductCommand(product));

            var response = result.Adapt<UpdateProductResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
