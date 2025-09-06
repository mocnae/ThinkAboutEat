using System;
using Recipe.Domain.Abstractions;
using Recipe.Domain.ValueObjects;

namespace Recipe.Domain.Models;

public class RecipeIngridient : Entity<RecipeIngridientId>
{
    public RecipeId RecipeId { get; set; }
    public string Name { get; set; } = default!;
    public int Gramm { get; set; }
}
