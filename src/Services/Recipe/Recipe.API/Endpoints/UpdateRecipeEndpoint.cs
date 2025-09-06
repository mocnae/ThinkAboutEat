using System;
using Carter;
using Mapster;
using MediatR;
using Recipe.Application.Dtos;
using Recipe.Application.Recipes.Commands.UpdateRecipe;

namespace Recipe.API.Endpoints;

public record UpdateRecipeRequest(RecipeDto RecipeDto);

public class UpdateRecipeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/recipes/", async (UpdateRecipeRequest requst, ISender sender) =>
        {
            var command = requst.Adapt<UpdateRecipeCommand>();

            var result = await sender.Send(command);

            return Results.Ok(result);
        })
        .WithSummary("UpdateRecipe")
        .WithDescription("UpdateRecipe")
        .Produces<bool>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("UpdateRecipe");
    }
}
