using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Products;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries.Products;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Products
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public GetAllProductsHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repo.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
