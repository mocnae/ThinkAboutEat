using System;
using BuildingBlocks.Exceptions;
using Carter;
using Mapster;
using MediatR;
using Recipe.Application.Dtos;
using Recipe.Application.Recipes.Commands.AddRecipe;

namespace Recipe.API.Endpoints;

public record CreateRecipeRequest(RecipeDto RecipeDto);

public record CreateRecipeResponse(Guid Id);

public class CreateRecipe : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/recipes/", async (CreateRecipeRequest request, ISender sender) =>
        {
            var command = request.Adapt<AddRecipeCommand>();

            if (command is null)
                throw new BadRequestException("invalid request model");

            var result = await sender.Send(command);

            var response = result.Adapt<CreateRecipeResponse>();

            if (response is null)
                return Results.BadRequest();

            return Results.Created($"/recipes/{response.Id}", response);
        })
        .WithName("CreateRecipe")
        .Produces<CreateRecipeResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Recipe")
        .WithDescription("Create Recipe");
    }
}
