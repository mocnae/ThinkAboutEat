using System;
using BuildingBlocks.CQRS;
using Recipe.Application.Dtos;

namespace Recipe.Application.Recipes.Queries.GetRecipes;

public record GetRecipesQuery(): IQuery<GetRecipesResult>;

public record GetRecipesResult(List<RecipeDto> Recipes);
