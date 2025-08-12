using System;

namespace ToDo.Domain.Common.Abstraction;

public interface IDomainEvent
{
    DateTime OccurredAt { get; }
}
