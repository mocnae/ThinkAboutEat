using System;
using Carter;
using MediatR;
using Recipe.Application.Recipes.Commands.DeleteRecipe;

namespace Recipe.API.Endpoints;

public class DeleteRecipeEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/recipes/{id:guid}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteRecipeCommand(id);

            var result = await sender.Send(command);

            return Results.Ok(result);
        })
        .WithName("DeleteRecipe")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Delete Recipe")
        .WithSummary("Delete Recipe");
    }
}
