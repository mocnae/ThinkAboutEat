using System;
using BuildingBlocks.Exceptions;
using Carter;
using MediatR;
using Product.API.Features.AddProduct.GetKBJUForRecipe;

namespace Product.API.Features.GetKBJUForRecipe;

public class GetKBJUEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products/getkbju", async (GetKBJUQuery request, ISender sender) =>
        {
            var result = await sender.Send(request);

            if (result is null)
                throw new BadRequestException("error in counting kbju");

            return Results.Ok(result.model);
        })
        .WithName("GetKBJU")
        .Produces<GetKBJUResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get kbju for products")
        .WithDescription("Get kbju for products");
    }
}
