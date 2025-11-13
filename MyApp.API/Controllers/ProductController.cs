using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands;
using MyApp.Application.Queries;
using System.Threading.Tasks;

namespace MyApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllProductsQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await _mediator.Send(new GetProductByIdQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand cmd) => Ok(await _mediator.Send(cmd));

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProductCommand cmd) => Ok(await _mediator.Send(cmd));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id) => Ok(await _mediator.Send(new DeleteProductCommand(id)));
}