using MediatR;
using MyApp.Application.DTOs.Products;
using System;

namespace MyApp.Application.Features.Products.Commands;

public record UpdateProductCommand(int Id, string Name, Int64 Price) : IRequest<ProductDto>;