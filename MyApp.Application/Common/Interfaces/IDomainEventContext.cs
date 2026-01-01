using MyApp.Domain.Common;
using System.Collections.Generic;

namespace MyApp.Application.Common.Interfaces
{
    public interface IDomainEventContext
    {
        IReadOnlyCollection<AggregateRoot> GetAggregatesWithEvents();
        void ClearDomainEvents();
    }
}
