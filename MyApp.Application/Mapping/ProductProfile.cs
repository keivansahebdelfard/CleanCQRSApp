using AutoMapper;
using MyApp.Application.DTOs.Products;
using MyApp.Domain.Entities;

namespace MyApp.Application.Mapping
{
    class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Entity → DTO
            CreateMap<Product, ProductDto>();

            // DTO → Entity (اگر برای Update نیاز داریم)
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
