using System;

namespace Recipe.Domain.ValueObjects;

public record RelatedRecipeId : IValueObject<Guid>
{
    public Guid Value { get; set; }

    private RelatedRecipeId(Guid id)
    {
        Value = id;
    }

    public static RelatedRecipeId Of(Guid id)
    {
        return new RelatedRecipeId(id);
    }
}
