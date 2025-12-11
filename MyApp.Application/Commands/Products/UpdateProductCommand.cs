using MediatR;
using MyApp.Application.DTOs.Products;

namespace MyApp.Application.Commands.Products;

public record UpdateProductCommand(int Id, string Name, decimal Price) : IRequest<ProductDto>;