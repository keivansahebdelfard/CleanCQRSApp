using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.HProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepository _repo;
        public CreateProductHandler(IProductRepository repo) => _repo = repo;

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product { Name = request.Name, Price = request.Price };
            await _repo.AddAsync(product);
            return product;
        }
    }

}
