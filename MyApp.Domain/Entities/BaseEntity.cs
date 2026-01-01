using MyApp.Domain.Events;
using System.Collections.Generic;

namespace MyApp.Domain.Entities
{
    public abstract class BaseEntity
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents() => _domainEvents.Clear();
    }

}
