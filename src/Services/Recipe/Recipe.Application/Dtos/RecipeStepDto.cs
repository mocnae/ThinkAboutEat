using System;

namespace Recipe.Application.Dtos;

public class RecipeStepDto
{
    // public Guid RecipeId { get; set; }
    public string Name { get; set; } = default!;
    public short StepNumber { get; set; }
    public string Description { get; set; } = default!;
    public string PhotoPath { get; set; } = default!;
}
