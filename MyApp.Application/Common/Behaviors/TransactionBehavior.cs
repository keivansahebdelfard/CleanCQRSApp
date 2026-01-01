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

            bool useTransaction = request is ITransactionalRequest;

            if (useTransaction)
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var response = await next();

                if (useTransaction)
                    await _unitOfWork.CommitAsync(cancellationToken);
                else
                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                // پردازش Domain Events
                var list = _eventContext.GetAggregatesWithEvents().ToList();
                var events = list.SelectMany(x => x.DomainEvents).ToList();
                list.ForEach(x => x.ClearDomainEvents());

                await _dispatcher.DispatchAsync(events, cancellationToken);

                return response;
            }
            catch
            {
                if (useTransaction)
                    await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
