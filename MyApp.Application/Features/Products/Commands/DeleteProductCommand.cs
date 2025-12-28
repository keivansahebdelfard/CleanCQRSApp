using MediatR;

namespace MyApp.Application.Features.Products.Commands;

public record DeleteProductCommand(int Id) : IRequest<bool>;