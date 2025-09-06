using System;

namespace Recipe.Application.Dtos;

public class RecipeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Kalor { get; set; }
    public decimal Belk { get; set; }
    public decimal Jir { get; set; }
    public decimal Uglev { get; set; }
    public List<RecipeIngridientDto> RecipeIngridients { get; set; } = new();
    public List<RecipeStepDto> RecipeSteps { get; set; } = new();
}
