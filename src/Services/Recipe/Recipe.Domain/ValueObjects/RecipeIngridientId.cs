using System;

namespace Recipe.Domain.ValueObjects;

public record RecipeIngridientId : IValueObject<Guid>
{
    public Guid Value { get; }

    private RecipeIngridientId(Guid id)
    {
        Value = id;
    }

    public static RecipeIngridientId Of(Guid id)
    {
        return new RecipeIngridientId(id);
    }
}
