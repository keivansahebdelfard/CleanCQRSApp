using MediatR;
using System;

namespace MyApp.Domain.Events
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
