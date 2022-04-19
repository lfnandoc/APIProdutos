using IntercambioGenebraAPI.Application.Commands.CreateProduct;
using IntercambioGenebraAPI.Application.Commands.DeleteProduct;
using IntercambioGenebraAPI.Application.Commands.UpdateProduct;
using IntercambioGenebraAPI.Application.Queries.GetAllProducts;
using IntercambioGenebraAPI.Application.Queries.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Api.Controllers
{
    [ApiController]
    [Route("product")]
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

            return response.GetResult();
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var response = await _mediator.Send(query);

            return response.GetResult();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
            var response = await _mediator.Send(command);
            
            return response.GetResult();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommand command, [FromRoute] Guid id)
        {
            if (command.Id != id)
                return UnprocessableEntity("Ids doesn't match."); 

            var response = await _mediator.Send(command);
            
            return response.GetResult();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new DeleteProductCommand(id);
            var response = await _mediator.Send(command);

            return response.GetResult();
        }
    }
}