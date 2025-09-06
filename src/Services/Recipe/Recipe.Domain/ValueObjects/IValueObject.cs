using System;

namespace Recipe.Domain.ValueObjects;

public interface IValueObject<T>
{
    public T Value { get; }
}
