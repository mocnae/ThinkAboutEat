using System;
using Mapster;
using Recipe.Application.Dtos;
using Recipe.Domain.Models;

namespace Recipe.Application.Extensions;

public static class RecipeExtnesions
{
    public static Recipe.Domain.Models.Recipe MapDtoToRecipe(this RecipeDto dto)
    {
        var recipe = Recipe.Domain.Models.Recipe.Create(
            dto.Name, dto.Description, dto.Kalor, dto.Belk, dto.Jir, dto.Uglev
        );

        dto.RecipeIngridients.ForEach(
            x => recipe.AddIngridient(x.Id, recipe.Id, x.Name, x.Gramm)
        );

        dto.RecipeSteps.ForEach(
            x => recipe.AddStep(recipe.Id, x.Name, x.StepNumber, x.Description, x.PhotoPath)
        );

        return recipe;
    }

    public static RecipeDto MapRecipeToDto(this Recipe.Domain.Models.Recipe recipe)
    {
        var dto = new RecipeDto
        {
            Id = recipe.Id.Value,
            Name = recipe.Name,
            Description = recipe.Description,
            Kalor = recipe.Kalor,
            Belk = recipe.Belk,
            Jir = recipe.Jir,
            Uglev = recipe.Uglev,
            RecipeIngridients = recipe.RecipeIngridients.Select(x => new RecipeIngridientDto
            {
                Id = x.Id.Value,
                Name = x.Name,
                Gramm = x.Gramm
            }).ToList(),  
            RecipeSteps = recipe.RecipeSteps.Select(x => new RecipeStepDto
            {
                Name = x.Name,
                Description = x.Description,
                StepNumber = x.StepNumber,
                PhotoPath = x.PhotoPath
            }).ToList()
        };

        return dto;
    }
}
