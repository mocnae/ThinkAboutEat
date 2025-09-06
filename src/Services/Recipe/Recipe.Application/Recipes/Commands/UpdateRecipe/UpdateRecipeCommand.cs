using System;
using BuildingBlocks.CQRS;
using Recipe.Application.Dtos;

namespace Recipe.Application.Recipes.Commands.UpdateRecipe;

public record UpdateRecipeCommand(RecipeDto RecipeDto): ICommand<UpdateRecipeResult>;

public record UpdateRecipeResult(bool isSuccess);
