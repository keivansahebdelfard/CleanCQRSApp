using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands.Products;
using MyApp.Application.Queries.Products;

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
            return View(); // برمی‌گرداند Views/Product/Create.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand cmd)
        {
            if (!ModelState.IsValid) return View(cmd);
            await _mediator.Send(cmd);
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
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
