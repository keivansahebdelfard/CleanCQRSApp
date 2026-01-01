using Microsoft.EntityFrameworkCore;
using MyApp.Application.Common.Interfaces;
using MyApp.Domain.Common;
using MyApp.Domain.Entities;
using MyApp.Domain.Events;
using System.Collections.Generic;
using System.Linq;

public sealed class AppDbContext : DbContext, IDomainEventContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // --------------------
    // DbSets
    // --------------------
    public DbSet<Product> Products => Set<Product>();

    // --------------------
    // Domain Events (in-memory)
    // --------------------
    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    // --------------------
    // EF Core Config
    // --------------------
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public IReadOnlyCollection<AggregateRoot> GetAggregatesWithEvents()
    {
        return ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();
    }
}
