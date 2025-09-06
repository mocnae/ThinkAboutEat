using System;
using Recipe.Domain.Abstractions;

namespace Recipe.Domain.Events;

public record IngridientsChangedEvent(Recipe.Domain.Models.Recipe recipe) : IDomainEvent
{

}
