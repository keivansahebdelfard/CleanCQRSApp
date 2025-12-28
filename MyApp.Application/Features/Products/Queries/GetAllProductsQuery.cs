using MediatR;
using MyApp.Application.DTOs.Products;
using System.Collections.Generic;

namespace MyApp.Application.Features.Products.Queries;

public record GetAllProductsQuery() : IRequest<List<ProductDto>>;