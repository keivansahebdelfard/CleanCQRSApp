using MediatR;
using MyApp.Application.DTOs.Products;
using System.Collections.Generic;

namespace MyApp.Application.Queries.Products;

public record GetAllProductsQuery() : IRequest<List<ProductDto>>;