using System;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.LMS.Domain.Primitives
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected AggregateRoot()
        {
        }

        protected AggregateRoot(Guid id) 
            : base(id)
        {
        }

        public void ClearDomainEvents() => _domainEvents.Clear();

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
