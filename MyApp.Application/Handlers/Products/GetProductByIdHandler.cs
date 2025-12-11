using AutoMapper;
using MediatR;
using MyApp.Application.DTOs.Products;
using MyApp.Application.Interfaces;
using MyApp.Application.Queries.Products;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Products
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _repo.GetByIdAsync(request.Id);
            if (product == null) return null;
            return _mapper.Map<ProductDto>(product);
        }
    }
}
