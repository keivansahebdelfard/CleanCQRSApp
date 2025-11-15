using MediatR;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries;
using MyApp.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.HProduct
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductRepository _repo;
        public GetProductByIdHandler(IProductRepository repo) => _repo = repo;

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetByIdAsync(request.Id);
        }
    }

}
