using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Products;
using MyApp.Application.DTOs.Products;
using MyApp.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Products
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);
            if (entity == null) return null;

            entity.Name = request.Name;
            entity.Price = request.Price;

            var updated = await _repo.UpdateAsync(entity);
            return _mapper.Map<ProductDto>(entity);
        }
    }
}
