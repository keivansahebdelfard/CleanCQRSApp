using MediatR;
using MyApp.Application.Common.Results;
using MyApp.Application.DTOs.Products;
using System;

namespace MyApp.Application.Features.Products.Commands;

public record CreateProductCommand(string Name, Int64 Price) : IRequest<Result<ProductDto>>;