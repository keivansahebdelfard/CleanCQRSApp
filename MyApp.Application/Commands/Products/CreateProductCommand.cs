using MediatR;
using MyApp.Application.DTOs.Products;

namespace MyApp.Application.Commands.Products;

public record CreateProductCommand(string Name, decimal Price) : IRequest<ProductDto>;