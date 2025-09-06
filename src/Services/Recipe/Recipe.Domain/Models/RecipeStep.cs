using System;
using Recipe.Domain.Abstractions;
using Recipe.Domain.ValueObjects;

namespace Recipe.Domain.Models;

public class RecipeStep : Entity<RecipeStepId>
{
    public RecipeId RecipeId { get; set; }
    public string Name { get; set; } = default!;
    public short StepNumber { get; set; }
    public string Description { get; set; } = default!;
    public string PhotoPath { get; set; } = default!;
}
