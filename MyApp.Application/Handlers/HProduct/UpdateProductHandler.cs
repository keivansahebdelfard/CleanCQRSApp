using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.HProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductRepository _repo;
        public UpdateProductHandler(IProductRepository repo) => _repo = repo;

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product { Id = request.Id, Name = request.Name, Price = request.Price };
            await _repo.UpdateAsync(product);
            return product;
        }
    }

}
