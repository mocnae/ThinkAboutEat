using System;
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Recipe.Application.Data;
using Recipe.Application.Extensions;

namespace Recipe.Application.Recipes.Queries.GetRecipeByName;

public class GetRecipeByNameHandler
    (IApplicationDbContext context)
    : IQueryHandler<GetRecipeByNameQuery, GetRecipeByNameResult>
{
    public async Task<GetRecipeByNameResult> Handle(GetRecipeByNameQuery request, CancellationToken cancellationToken)
    {
        var recipes = context.Recipes
            .Where(x => x.Name.ToLower().Contains(request.name.ToLower()))
            .AsNoTracking();

        return new GetRecipeByNameResult(await recipes.Select(x => x.MapRecipeToDto()).ToListAsync());
    }
}
