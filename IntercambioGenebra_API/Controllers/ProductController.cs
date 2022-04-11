using IntercambioGenebraAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IntercambioGenebraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProducts")]
        public IEnumerable<Product> Get()
        {
            return new Product().GetAllMapped().OfType<Product>();
        }
    }
}