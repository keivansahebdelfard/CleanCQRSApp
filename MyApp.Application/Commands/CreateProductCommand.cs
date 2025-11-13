using MediatR;
using MyApp.Domain.Entities;

namespace MyApp.Application.Commands;

public record CreateProductCommand(string Name, decimal Price) : IRequest<Product>;