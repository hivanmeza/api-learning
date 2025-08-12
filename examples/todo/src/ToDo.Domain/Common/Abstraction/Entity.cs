using System;

namespace ToDo.Domain.Common.Abstraction;

public abstract class Entity
{
    public Guid Id { get; private set; }

    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity(Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
    }

    protected void Raise(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    public override bool Equals(object? obj)
    => obj is Entity other && other.Id == Id;

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Entity? a, Entity? b)
    => a is null && b is null || !(a is null) && a.Equals(b);

    public static bool operator !=(Entity? a, Entity? b)
    => !(a == b);
}
