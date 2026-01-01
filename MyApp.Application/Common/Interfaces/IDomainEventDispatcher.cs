using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Domain.Events
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default);
    }
}
