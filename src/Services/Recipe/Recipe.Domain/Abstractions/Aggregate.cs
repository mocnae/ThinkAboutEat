using System;

namespace Recipe.Domain.Abstractions;

public class Aggregate<T> : IAggregate<T>
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public T Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

    public IDomainEvent[] ClearDomainEvents()
    {
        var dispatchedEvents = _domainEvents.ToArray();

        _domainEvents.Clear();

        return dispatchedEvents;
    }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
