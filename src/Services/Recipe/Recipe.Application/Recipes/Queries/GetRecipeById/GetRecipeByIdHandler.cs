using System;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Recipe.Application.Data;
using Recipe.Application.Extensions;
using Recipe.Domain.ValueObjects;

namespace Recipe.Application.Recipes.Queries.GetRecipeById;

public class GetRecipeByIdHandler
    (IApplicationDbContext context)
    : IQueryHandler<GetRecipeByIdQuery, GetRecipeByIdResult>
{
    public async Task<GetRecipeByIdResult> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
    {
        var recipe = await context.Recipes
            .Include(x => x.RecipeIngridients)
            .Include(x => x.RecipeSteps)
            .FirstOrDefaultAsync(x => x.Id == RecipeId.Of(request.Id));

        if (recipe is null)
            throw new NotFoundException("recipe not found");

        return new GetRecipeByIdResult(recipe.MapRecipeToDto());
    }
}
