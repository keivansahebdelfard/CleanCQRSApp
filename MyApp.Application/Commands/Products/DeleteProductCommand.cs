using MediatR;

namespace MyApp.Application.Commands.Products;

public record DeleteProductCommand(int Id) : IRequest<bool>;