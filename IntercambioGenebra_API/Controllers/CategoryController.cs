using IntercambioGenebraAPI.Commands.Category;
using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {

        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryRepository _repository;
        private readonly IMediator _mediator;

        public CategoryController(ILogger<CategoryController> logger, ICategoryRepository repository, IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get([FromRoute] Guid id)
        {
            return Ok(_repository.GetCategoryByIdAsync(id));
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand command)
        {
            var response = await _mediator.Send(command);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Result);
        }

    }
}