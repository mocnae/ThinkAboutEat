using System;
using BuildingBlocks.CQRS;
using Recipe.Application.Dtos;

namespace Recipe.Application.Recipes.Queries.GetRecipeByName;

public record GetRecipeByNameQuery(string name): IQuery<GetRecipeByNameResult>;

public record GetRecipeByNameResult(List<RecipeDto> RecipeDtos);
