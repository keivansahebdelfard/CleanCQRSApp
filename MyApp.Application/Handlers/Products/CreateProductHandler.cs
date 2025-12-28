using AutoMapper;
using MediatR;
using MyApp.Application.Common.Interfaces;
using MyApp.Application.DTOs.Products;
using MyApp.Application.Features.Products.Commands;
using MyApp.Domain.Entities;
using MyApp.Domain.Events.Products;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            // ذخیره در دیتابیس
            var created = await _repo.AddAsync(entity);

            // گام ۵: اضافه کردن Domain Event  
            entity.AddDomainEvent(new ProductCreatedEvent(entity));

            return _mapper.Map<ProductDto>(created);
        }
    }
}
