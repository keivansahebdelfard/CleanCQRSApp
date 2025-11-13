using MediatR;
using MyApp.Domain.Entities;

namespace MyApp.Application.Commands;

public record UpdateProductCommand(int Id, string Name, decimal Price) : IRequest<Product>;