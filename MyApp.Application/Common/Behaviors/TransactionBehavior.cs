using MediatR;
using MyApp.Application.Common.Interfaces;
using MyApp.Domain.Events;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Common.Behaviors
{
    public sealed class TransactionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainEventContext _eventContext;
        private readonly IDomainEventDispatcher _dispatcher;

        public TransactionBehavior(
            IUnitOfWork unitOfWork,
            IDomainEventContext eventContext,
            IDomainEventDispatcher dispatcher)
        {
            _unitOfWork = unitOfWork;
            _eventContext = eventContext;
            _dispatcher = dispatcher;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("TRANSACTION BEHAVIOR HIT");

            var response = await next();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var aggregates = _eventContext.GetAggregatesWithEvents();

            var events = aggregates
                .SelectMany(x => x.DomainEvents)
                .ToList();

            aggregates.ToList().ForEach(x => x.ClearDomainEvents());

            await _dispatcher.DispatchAsync(events, cancellationToken);

            return response;
        }
    }
}
