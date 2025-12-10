using MediatR;
using MyApp.Domain.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Class
{
    public class DomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Dispatch(IEnumerable<IDomainEvent> events)
        {
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
