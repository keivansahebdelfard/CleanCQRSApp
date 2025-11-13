using MediatR;
using MyApp.Domain.Entities;

namespace MyApp.Application.Queries;

public record GetProductByIdQuery(int Id) : IRequest<Product>;