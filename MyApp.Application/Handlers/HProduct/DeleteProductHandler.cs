using MediatR;
using MyApp.Application.Commands;
using MyApp.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.HProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repo;
        public DeleteProductHandler(IProductRepository repo) => _repo = repo;

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);
            if (entity == null) return false;

            await _repo.DeleteAsync(request.Id);
            return true;
        }
    }
}
