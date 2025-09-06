using System;
using BuildingBlocks.CQRS;
using Recipe.Application.Dtos;

namespace Recipe.Application.Recipes.Queries.GetRecipeById;

public record GetRecipeByIdQuery(Guid Id): IQuery<GetRecipeByIdResult>;

public record GetRecipeByIdResult(RecipeDto RecipeDto);
