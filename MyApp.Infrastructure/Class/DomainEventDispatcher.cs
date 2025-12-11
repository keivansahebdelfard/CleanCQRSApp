using MediatR;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.DomainEvents
{
    public class DomainEventDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchEventsAsync(DbContext ctx)
        {
            var entities = ctx.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity)
                .ToList();

            foreach (var entity in entities)
            {
                var events = entity.DomainEvents.ToList();
                entity.ClearDomainEvents();

                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent);
                }
            }
        }
    }
}
