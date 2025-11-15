using MediatR;
using MyApp.Application.DTOs.Product;

namespace MyApp.Application.Commands;

public record UpdateProductCommand(int Id, string Name, decimal Price) : IRequest<ProductDto>;