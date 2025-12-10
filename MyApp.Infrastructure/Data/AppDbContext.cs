using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.Class;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    private readonly DomainEventDispatcher _dispatcher;

    public AppDbContext(DbContextOptions<AppDbContext> options, DomainEventDispatcher dispatcher)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    public DbSet<Product> Products => Set<Product>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        var entities = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        var events = entities.SelectMany(e => e.DomainEvents).ToList();

        foreach (var entity in entities)
            entity.ClearDomainEvents();

        await _dispatcher.Dispatch(events);

        return result;
    }
}