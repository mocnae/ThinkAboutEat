using System;
using Carter;
using MediatR;
using Recipe.Application.Dtos;
using Recipe.Application.Recipes.Queries.GetRecipeById;

namespace Recipe.API.Endpoints;

public class GetRecipeByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/recipes/{id:guid}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetRecipeByIdQuery(id));

            return Results.Ok(result);
        })
        .WithName("GetRecipeById")
        .Produces<RecipeDto>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get recipe by id")
        .WithDescription("Get recipe by id");
    }
}
