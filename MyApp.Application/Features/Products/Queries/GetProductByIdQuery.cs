using MediatR;
using MyApp.Application.DTOs.Products;

namespace MyApp.Application.Features.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;