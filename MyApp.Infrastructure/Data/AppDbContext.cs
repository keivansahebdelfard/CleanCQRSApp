using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Infrastructure.DomainEvents;
using System.Threading;
using System.Threading.Tasks;

public class AppDbContext : DbContext
{
    private readonly DomainEventDispatcher _dispatcher;

    public AppDbContext(DbContextOptions<AppDbContext> options,
                        DomainEventDispatcher dispatcher)
        : base(options)
    {
        _dispatcher = dispatcher;
    }

    public DbSet<Product> Products => Set<Product>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken);

        await _dispatcher.DispatchEventsAsync(this);

        return result;
    }
}
