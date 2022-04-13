using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }

    }
}