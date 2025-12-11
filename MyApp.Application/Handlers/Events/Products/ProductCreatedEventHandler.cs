using MyApp.Domain.Events;
using MyApp.Domain.Events.Products;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Events.Products
{
    public class ProductCreatedEventHandler : IDomainEvent
    {
        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            // اینجا هر عملیاتی که پس از ایجاد محصول باید انجام شود را می‌نویسیم
            // مثل ارسال ایمیل، لاگ کردن، Publish به RabbitMQ، SignalR و ...

            Console.WriteLine($"EVENT FIRED => محصول جدید ایجاد شد: {notification.Product.Name}");

            return Task.CompletedTask;
        }
    }
}
