using System;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Recipe.Application.Data;
using Recipe.Domain.ValueObjects;

namespace Recipe.Application.Recipes.Commands.DeleteRecipe;

public class DeleteRecipeHandler
    (IApplicationDbContext context)
    : ICommandHandler<DeleteRecipeCommand, DeleteRecipeResult>
{
    public async Task<DeleteRecipeResult> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeId = RecipeId.Of(request.Id);
        var recipe = await context.Recipes.FindAsync([recipeId], cancellationToken);

        if (recipe is null)
            throw new NotFoundException("recipe not found");

        context.Recipes.Remove(recipe);
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteRecipeResult(true);
    }
}
