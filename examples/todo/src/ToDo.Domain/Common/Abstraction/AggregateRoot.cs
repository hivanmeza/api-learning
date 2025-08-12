using System;

namespace ToDo.Domain.Common.Abstraction;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid? id = null) : base(id) { }
}
