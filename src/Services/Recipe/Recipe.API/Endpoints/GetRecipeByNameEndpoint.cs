using System;
using Carter;
using MediatR;
using Recipe.Application.Dtos;
using Recipe.Application.Recipes.Queries.GetRecipeByName;

namespace Recipe.API.Endpoints;

public class GetRecipeByNameEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/recipes/{name:alpha}", async (string name, ISender sender) =>
        {
            var result = await sender.Send(new GetRecipeByNameQuery(name));

            return Results.Ok(result);
        })
        .WithName("GetRecipeByName")
        .Produces<List<RecipeDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Get Recipe By Name")
        .WithSummary("Get Recipe By Name");
    }
}
