using System;

namespace Recipe.Domain.ValueObjects;

public record RecipeStepId : IValueObject<Guid>
{
    public Guid Value { get; }

    private RecipeStepId(Guid id)
    {
        Value = id;
    }

    public static RecipeStepId Of(Guid id)
    {
        return new RecipeStepId(id);
    }
}