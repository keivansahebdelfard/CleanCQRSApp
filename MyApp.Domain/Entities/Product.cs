using System;

namespace MyApp.Domain.Entities;

public class Product : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Int64 Price { get; set; }
}