using MyApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Common.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Product> AddAsync(Product product, System.Threading.CancellationToken cancellationToken);
    Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

    // Expose IQueryable for advanced queries (projection, pagination)
    IQueryable<Product> Query();

    // Return a paged list of products (avoids adding EF Core dependency to Application layer)
    Task<List<Product>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);
}