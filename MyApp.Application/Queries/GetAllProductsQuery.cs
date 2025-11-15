using MediatR;
using MyApp.Application.DTOs.Product;
using System.Collections.Generic;

namespace MyApp.Application.Queries;

public record GetAllProductsQuery() : IRequest<List<ProductDto>>;