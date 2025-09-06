using System;
using BuildingBlocks.CQRS;
using BuildingBlocks.Exceptions;
using Microsoft.EntityFrameworkCore;
using Recipe.Application.Data;
using Recipe.Domain.ValueObjects;

namespace Recipe.Application.Recipes.Commands.UpdateRecipe;

public class UpdateRecipeHandler
    (IApplicationDbContext context)
    : ICommandHandler<UpdateRecipeCommand, UpdateRecipeResult>
{
    public async Task<UpdateRecipeResult> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await context.Recipes
            .Include(x => x.RecipeSteps)
            .Include(x => x.RecipeIngridients)
            .FirstOrDefaultAsync(x => x.Id == RecipeId.Of(request.RecipeDto.Id));

        if (recipe is null)
            throw new NotFoundException("recipe not found");

        recipe.Update(
            request.RecipeDto.Name, request.RecipeDto.Description, request.RecipeDto.Kalor, request.RecipeDto.Belk, request.RecipeDto.Jir, request.RecipeDto.Uglev
        );

        var notContainsIngridients = request.RecipeDto.RecipeIngridients
            .Select(x => x.Id)
            .Except(recipe.RecipeIngridients.Select(x => x.Id.Value));

        foreach (var i in notContainsIngridients)
        {
            var ingridient = request.RecipeDto.RecipeIngridients.FirstOrDefault(x => x.Id == i);

            recipe.AddIngridient(ingridient!.Id, recipe.Id, ingridient.Name, ingridient.Gramm);
        }

        recipe.RecipeSteps
            .Select(x => x.Id)
            .ToList()
            .ForEach(x => recipe.RemoveStep(x));

        foreach (var step in request.RecipeDto.RecipeSteps)
        {
            recipe.AddStep(recipe.Id, step.Name, step.StepNumber, step.Description, step.PhotoPath);
        }

        await context.SaveChangesAsync(cancellationToken);

        return new UpdateRecipeResult(true);
    }
}
