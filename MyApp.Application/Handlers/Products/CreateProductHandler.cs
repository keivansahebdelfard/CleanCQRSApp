using AutoMapper;
using MediatR;
using MyApp.Application.Common.Interfaces;
using MyApp.Application.Common.Results;
using MyApp.Application.DTOs.Products;
using MyApp.Application.Features.Products.Commands;
using MyApp.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request.Name, request.Price);

            // 3. Persist
            var created = await _repo.AddAsync(product, cancellationToken);

            if (created is null)
            {
                return Result<ProductDto>.Failure(
                    new Error(
                        "CreateProductFailed",
                        "خطا در ذخیره‌سازی محصول"
                    ));
            }

            // 4. Mapping فقط در لبه خروجی
            var dto = _mapper.Map<ProductDto>(created);

            return Result<ProductDto>.Success(dto);
        }
    }
}
