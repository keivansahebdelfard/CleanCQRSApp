using MyApp.Domain.Entities;
using System;

namespace MyApp.Domain.Events.Products
{
    public sealed class ProductCreatedEvent : IDomainEvent
    {
        public Product Product { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;

        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
    }
}
