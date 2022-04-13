using IntercambioGenebraAPI.Commands.CreateProduct;
using IntercambioGenebraAPI.Commands.DeleteProduct;
using IntercambioGenebraAPI.Commands.UpdateProduct;
using IntercambioGenebraAPI.Queries.GetAllProducts;
using IntercambioGenebraAPI.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;

        public ProductController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllProductsQuery();
            var response = await _mediator.Send(query);

            return response.Result;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var response = await _mediator.Send(query);

            return response.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return response.Result;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommand command, [FromRoute] Guid id)
        {
            if (command.Id != id)
            {
                return UnprocessableEntity("Ids doesn't match.");
            }

            var response = await _mediator.Send(command);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return response.Result;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new DeleteProductCommand(id);

            var response = await _mediator.Send(command);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return response.Result;
        }

    }
}