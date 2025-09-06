using System;
using BuildingBlocks.CQRS;
using FluentValidation;
using Recipe.Application.Data;
using Recipe.Application.Dtos;

namespace Recipe.Application.Recipes.Commands.AddRecipe;

public class AddRecipeCommandValidator : AbstractValidator<AddRecipeCommand>
{
    public AddRecipeCommandValidator()
    {
        RuleFor(x => x.RecipeDto.Name).Length(1, 255).WithMessage("длина наименования рецепта должна быть не менее 1 сивола и не более 255");
        RuleFor(x => x.RecipeDto.RecipeIngridients).NotEmpty().WithMessage("список ингридиентов не может быть пустым");
        RuleFor(x => x.RecipeDto.RecipeSteps).NotEmpty().WithMessage("список шагов приготовления рецепта не может быть пустым");
    }
}

public class AddRecipeEventHandler
    (IApplicationDbContext context)
    : ICommandHandler<AddRecipeCommand, AddRecipeResult>
{
    public async Task<AddRecipeResult> Handle(AddRecipeCommand command, CancellationToken cancellationToken)
    {
        Recipe.Domain.Models.Recipe recipe = default!;

        try
        {
            recipe = MapToRecipe(command.RecipeDto);

            await context.Recipes.AddAsync(recipe);

            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return new AddRecipeResult(Guid.Empty);
        }

        return new AddRecipeResult(recipe.Id.Value);
    }

    public Recipe.Domain.Models.Recipe MapToRecipe(RecipeDto dto)
    {
        var recipe = Recipe.Domain.Models.Recipe.Create(
            dto.Name, dto.Description, dto.Kalor, dto.Belk, dto.Jir, dto.Uglev
        );

        dto.RecipeIngridients.ForEach(x => recipe.AddIngridient(
            x.Id, recipe.Id, x.Name, x.Gramm
        ));

        dto.RecipeSteps.ForEach(x => recipe.AddStep(
            recipe.Id, x.Name, x.StepNumber, x.Description, x.PhotoPath
        ));

        return recipe;
    }
}
