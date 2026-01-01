using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyApp.Application.Common.Interfaces;
using MyApp.Application.DTOs.Products;

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
            app.MapGet("/api/product/getAll", async (int? page, int? pageSize, IMediator mediator, System.Threading.CancellationToken cancellationToken) =>
            {
                var products = await mediator.Send(new MyApp.Application.Features.Products.Queries.GetAllProductsQuery(page ?? 1, pageSize ?? 50), cancellationToken);
                return Results.Ok(products);
            });

            app.MapPost("/api/product/getbyId", async (int id, IProductRepository service, System.Threading.CancellationToken cancellationToken) =>
            {
                var product = await service.GetByIdAsync(id, cancellationToken);
                if (product == null) return Results.NotFound();
                return Results.Ok(product);
            });

            app.MapPost("/api/product/add", async (ProductDto product, IProductRepository service) =>
            {
                //await service.AddAsync();
                return Results.Created($"/api/sales/{product.Id}", product);
            });
        }
    }
}
