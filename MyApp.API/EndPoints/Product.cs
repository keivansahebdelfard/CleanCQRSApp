using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyApp.Application.Common.Interfaces;
using MyApp.Application.DTOs.Products;
using System;

namespace MyApp.API.EndPoints
{
    /// <summary>
    /// Minimal API هستند
    /// دیگر نیازی به کنترلر ندارند و مستقیم متد رو اجرا می کنند
    /// دو راه متفاوت فراخوانی متد ها مینیمال ای پی آی ها و مدیاتور ها هستند
    /// </summary>
    public static class ProdcutEndpoints
    {
        public static void MapEndpoints(this WebApplication app)
        {
            app.MapGet("/api/product/getAll", async (DateTime start, DateTime end, IProductRepository service) =>
            {
                var sales = await service.GetAllAsync();
                return Results.Ok(sales);
            });

            app.MapPost("/api/product/getbyId", async (int id, IProductRepository service) =>
            {
                await service.GetByIdAsync(id);
                return Results.Ok();
            });

            app.MapPost("/api/product/add", async (ProductDto product, IProductRepository service) =>
            {
                //await service.AddAsync();
                return Results.Created($"/api/sales/{product.Id}", product);
            });
        }
    }
}
