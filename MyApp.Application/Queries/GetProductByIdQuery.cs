using MediatR;
using MyApp.Application.DTOs.Product;

namespace MyApp.Application.Queries;

public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;