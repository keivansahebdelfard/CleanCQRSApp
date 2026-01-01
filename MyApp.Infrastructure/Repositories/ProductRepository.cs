using Microsoft.EntityFrameworkCore;
using MyApp.Application.Common.Interfaces;
using MyApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db) => _db = db;

    public async Task<List<Product>> GetAllAsync()
        => await _db.Products.ToListAsync();

    public async Task<Product> GetByIdAsync(int id)
        => await _db.Products.FindAsync(id)
           ?? throw new KeyNotFoundException("Product not found");

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
        var entity = await _db.Products.FindAsync(id);
        if (entity == null) return false;

        _db.Products.Remove(entity);  // فقط حذف
        return true;
    }
}

