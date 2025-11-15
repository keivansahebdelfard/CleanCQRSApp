using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.DTOs.Product;
using MyApp.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.HProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repo;
        public UpdateProductHandler(IProductRepository repo) => _repo = repo;

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);
            if (entity == null) return null;

            entity.Name = request.Name;
            entity.Price = request.Price;

            var updated = await _repo.UpdateAsync(entity);
            return new ProductDto(updated.Id, updated.Name, updated.Price);
        }
    }
}
