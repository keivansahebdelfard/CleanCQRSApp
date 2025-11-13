using MediatR;
using MyApp.Domain.Entities;
using System.Collections.Generic;

namespace MyApp.Application.Queries;

public record GetAllProductsQuery() : IRequest<List<Product>>;