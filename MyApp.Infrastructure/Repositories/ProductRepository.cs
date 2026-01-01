using Microsoft.EntityFrameworkCore;
using MyApp.Application.Common.Interfaces;
using MyApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db) => _db = db;

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _db.Products.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _db.Products.FindAsync(new object[] { id }, cancellationToken);

    public Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
    {
        _db.Products.Add(product);  // فقط اضافه می‌کنیم
        return Task.FromResult(product);
    }

    public Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        _db.Products.Update(product); // فقط Update
        return Task.FromResult(product);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _db.Products.FindAsync(new object[] { id }, cancellationToken);
        if (entity == null) return false;

        _db.Products.Remove(entity);  // فقط حذف
        return true;
    }

    public IQueryable<Product> Query() => _db.Products.AsNoTracking();

    public async Task<List<Product>> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var p = page < 1 ? 1 : page;
        var s = pageSize < 1 ? 50 : pageSize;
        return await _db.Products
            .AsNoTracking()
            .Skip((p - 1) * s)
            .Take(s)
            .ToListAsync(cancellationToken);
    }
}

