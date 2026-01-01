using MediatR;
using MyApp.Application.Common.Interfaces;
using MyApp.Application.Features.Products.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Products
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repo;
        public DeleteProductHandler(IProductRepository repo) => _repo = repo;

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (entity == null) return false;

            await _repo.DeleteAsync(request.Id, cancellationToken);
            return true;
        }
    }
}
