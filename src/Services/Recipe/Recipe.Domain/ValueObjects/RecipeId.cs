using System;

namespace Recipe.Domain.ValueObjects;

public record RecipeId : IValueObject<Guid>
{
    public Guid Value { get; }

    private RecipeId(Guid Id)
    {
        Value = Id;
    }

    public static RecipeId Of(Guid Id)
    {
        return new RecipeId(Id);
    }
}
