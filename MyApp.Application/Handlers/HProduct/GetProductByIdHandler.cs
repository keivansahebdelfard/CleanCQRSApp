using MediatR;
using MyApp.Application.DTOs.Product;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.HProduct
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repo;
        public GetProductByIdHandler(IProductRepository repo) => _repo = repo;

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repo.GetByIdAsync(request.Id);
            if (product == null) return null;
            return new ProductDto(product.Id, product.Name, product.Price);
        }
    }
}
