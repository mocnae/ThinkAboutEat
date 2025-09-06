using System;

namespace Recipe.Application.Dtos;

public class RecipeIngridientDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int Gramm { get; set; }
}
