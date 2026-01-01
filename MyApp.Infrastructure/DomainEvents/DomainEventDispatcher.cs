using MediatR;
using MyApp.Domain.Events;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.DomainEvents
{
    public sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAsync(
            IEnumerable<IDomainEvent> events,
            CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
