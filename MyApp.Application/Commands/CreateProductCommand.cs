using MediatR;
using MyApp.Application.DTOs.Product;

namespace MyApp.Application.Commands;

public record CreateProductCommand(string Name, decimal Price) : IRequest<ProductDto>;