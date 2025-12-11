using MediatR;
using MyApp.Application.DTOs.Products;

namespace MyApp.Application.Queries.Products;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;