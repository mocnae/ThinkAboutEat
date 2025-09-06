using System;
using BuildingBlocks.CQRS;
using Recipe.Application.Dtos;

namespace Recipe.Application.Recipes.Commands.AddRecipe;

public record AddRecipeCommand(RecipeDto RecipeDto): ICommand<AddRecipeResult>;

public record AddRecipeResult(Guid Id);
