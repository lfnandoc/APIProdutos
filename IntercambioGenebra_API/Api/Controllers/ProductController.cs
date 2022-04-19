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
        [Route("{productId:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid productId)
        {
            var query = new GetProductByIdQuery(productId);
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
        [Route("{productId:Guid}")]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommand command, [FromRoute] Guid productId)
        {
            if (command.Id != productId)
                return UnprocessableEntity("Ids doesn't match."); 

            var response = await _mediator.Send(command);
            
            return response.GetResult();
        }

        [HttpDelete]
        [Route("{productId:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid productId)
        {
            var command = new DeleteProductCommand(productId);
            var response = await _mediator.Send(command);

            return response.GetResult();
        }
    }
}