using IntercambioGenebraAPI.Application.Commands.CreateCategory;
using IntercambioGenebraAPI.Application.Commands.DeleteCategory;
using IntercambioGenebraAPI.Application.Commands.UpdateCategory;
using IntercambioGenebraAPI.Application.Queries.GetAllCategories;
using IntercambioGenebraAPI.Application.Queries.GetCategoryById;
using IntercambioGenebraAPI.Application.Queries.GetProductsByCategoryId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Api.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IMediator _mediator;

        public CategoryController(ILogger<CategoryController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCategoriesQuery();
            var response = await _mediator.Send(query);

            return response.GetResult();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var query = new GetCategoryByIdQuery(id);
            var response = await _mediator.Send(query);

            return response.GetResult();
        }

        [HttpGet]
        [Route("{id}/products")]
        public async Task<IActionResult> GetProducts([FromRoute] Guid id)
        {
            var query = new GetProductsByCategoryIdQuery(id);
            var response = await _mediator.Send(query);

            return response.GetResult();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand command)
        {
            var response = await _mediator.Send(command);
            
            return response.GetResult();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryCommand command, [FromRoute] Guid id)
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
            var command = new DeleteCategoryCommand(id);
            var response = await _mediator.Send(command);

            return response.GetResult();
        }
    }
}