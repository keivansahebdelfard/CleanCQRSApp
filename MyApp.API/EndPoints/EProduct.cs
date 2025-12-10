using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyApp.Application.DTOs.Product;
using MyApp.Application.Interfaces;
using System;

namespace MyApp.API.EndPoints
{
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
