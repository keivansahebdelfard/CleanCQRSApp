using MediatR;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries;
using MyApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.HProduct
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IProductRepository _repo;
        public GetAllProductsHandler(IProductRepository repo) => _repo = repo;

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }

}
