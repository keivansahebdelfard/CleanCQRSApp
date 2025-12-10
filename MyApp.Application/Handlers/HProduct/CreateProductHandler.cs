using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.DTOs.Product;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.HProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repo;

        public CreateProductHandler(IProductRepository repo) => _repo = repo;

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

            return new ProductDto(created.Id, created.Name, created.Price);
        }
    }
}
