using System;
using System.Windows.Input;
using BuildingBlocks.CQRS;

namespace Recipe.Application.Recipes.Commands.DeleteRecipe;

public record DeleteRecipeCommand(Guid Id): ICommand<DeleteRecipeResult>;

public record DeleteRecipeResult(bool isSuccess);