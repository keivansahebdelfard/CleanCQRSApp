using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Features.Products.Commands;
using MyApp.Application.Features.Products.Queries;

namespace MyApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator) => _mediator = mediator;

        public async Task<IActionResult> Index()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return View(products);
        }

        // GET: نمایش فرم ایجاد محصول
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            if (result.IsFailure)
            {
                ModelState.AddModelError(string.Empty, result.Error!.Message);
                return View(cmd);
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return View(new UpdateProductCommand(product.Id, product.Name, product.Price));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductCommand cmd)
        {
            if (!ModelState.IsValid) return View(cmd);
            await _mediator.Send(cmd);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _mediator.Send(new DeleteProductCommand(id));
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
