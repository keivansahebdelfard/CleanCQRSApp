using MyApp.Domain.Entities;

namespace MyApp.Domain.Events.Products
{
    public class ProductCreatedEvent : IDomainEvent
    {
        public Product Product { get; }

        public ProductCreatedEvent(Product product)
        {
            Product = product;
        }
    }
}
