using CommerceHub.Application.Features.Products.Commands.CreateProduct;
using CommerceHub.Application.Features.Products.Commands.DeleteProduct;
using CommerceHub.Application.Features.Products.Queries.GetAllProducts;
using CommerceHub.Application.Features.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommerceHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            // MediatR routes the command to the CreateProductCommandHandler we built earlier
            var productId = await _mediator.Send(command);

            return Ok(new { Id = productId, Message = "Product created successfully." });
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);

            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var query = new GetProductByIdQuery { Id = id };
            var product = await _mediator.Send(query);

            return Ok(product);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            var command = new DeleteProductCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
