using System;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Recipe.Application.Dtos;
using Recipe.Application.Recipes.Queries.GetRecipes;

namespace Recipe.API.Endpoints;

public class GetRecipes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/recipes", async (ISender sender) =>
        {
            var result = await sender.Send(new GetRecipesQuery());

            return Results.Ok(result);
        })
        .WithName("GetRecipes")
        .Produces<List<RecipeDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Recipes")
        .WithDescription("Get Recipes");
    }
}
