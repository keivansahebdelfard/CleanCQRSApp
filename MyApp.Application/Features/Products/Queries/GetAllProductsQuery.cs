using MediatR;
using MyApp.Application.DTOs.Products;
using System.Collections.Generic;

namespace MyApp.Application.Features.Products.Queries;

public record GetAllProductsQuery(int Page = 1, int PageSize = 50) : IRequest<List<ProductDto>>;