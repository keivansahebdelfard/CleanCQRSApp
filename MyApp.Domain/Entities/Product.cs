using MyApp.Domain.Common;
using MyApp.Domain.Events.Products;
using System;
namespace MyApp.Domain.Entities;

public class Product : AggregateRoot
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Int64 Price { get; set; }

    private Product() { } // EF

    public Product(string name, Int64 price)
    {
        Name = name;
        Price = price;
    }

    public static Product Create(string name, Int64 price)
    {
        var product = new Product(name, price);
        product.AddDomainEvent(new ProductCreatedEvent(product));
        return product;
    }
}