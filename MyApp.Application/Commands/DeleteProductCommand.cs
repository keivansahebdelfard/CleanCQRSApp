using MediatR;

namespace MyApp.Application.Commands;

public record DeleteProductCommand(int Id) : IRequest<bool>;