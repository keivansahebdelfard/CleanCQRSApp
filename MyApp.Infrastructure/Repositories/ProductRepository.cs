using Microsoft.EntityFrameworkCore;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;
    public ProductRepository(AppDbContext db) => _db = db;

    public async Task<List<Product>> GetAllAsync() => await _db.Products.ToListAsync();
    public async Task<Product> GetByIdAsync(int id) => await _db.Products.FindAsync(id) ?? throw new KeyNotFoundException("Product not found");
    public async Task<Product> AddAsync(Product product) { _db.Products.Add(product); await _db.SaveChangesAsync(); return product; }
    public async Task<Product> UpdateAsync(Product product) { _db.Products.Update(product); await _db.SaveChangesAsync(); return product; }
    public async Task<bool> DeleteAsync(int id) { var entity = await _db.Products.FindAsync(id); if (entity == null) return false; _db.Products.Remove(entity); await _db.SaveChangesAsync(); return true; }
}