using System;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Recipe.Application.Data;
using Recipe.Application.Dtos;
using Recipe.Application.Extensions;

namespace Recipe.Application.Recipes.Queries.GetRecipes;

public class GetRecipesQueryHandler
    (IApplicationDbContext context)
    : IQueryHandler<GetRecipesQuery, GetRecipesResult>
{
    public async Task<GetRecipesResult> Handle(GetRecipesQuery query, CancellationToken cancellationToken)
    {
        var recipes = await context.Recipes
            .Include(x => x.RecipeIngridients)
            .Include(x => x.RecipeSteps)
            .ToListAsync();

        if (!recipes.Any())
            throw new NotFoundException("не найдено рецептов в базе данных");

        return new GetRecipesResult(recipes.Select(x => x.MapRecipeToDto()).ToList());
    }
}
