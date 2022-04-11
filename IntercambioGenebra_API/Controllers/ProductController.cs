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

        [HttpGet("{id}", Name = "GetProductById")]
        public Product? Get(int id)
        {
            return new Product().GetById(id) as Product;
        }

        [HttpPost(Name = "AddProduct")]
        public IActionResult Post(Product product)
        {
            var userSubmittedId = product.Id != -2;

            if (userSubmittedId)
                return BadRequest("Custom product ids are not allowed.");

            var invalidProductName = product.Name == null || string.IsNullOrWhiteSpace(product.Name);

            if (invalidProductName)
                return BadRequest("Invalid product name.");

            product.Price ??= 0;

            if (product.Save())
                return CreatedAtRoute("GetProductById", new { id = product.Id }, product);

            return BadRequest("Product could not be saved.");
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        public IActionResult Put(int id, Product submittedProduct)
        {
            var existingProduct = new Product().GetById(id) as Product;

            if (existingProduct == null)
                return NotFound();

            var userSubmittedId = submittedProduct.Id != -2;

            if (userSubmittedId)
                return BadRequest("Updating product ids is not allowed.");

            var newProductName = submittedProduct.Name != null;
            
            if (newProductName)
            {
                var invalidProductName = string.IsNullOrWhiteSpace(submittedProduct.Name);
                if (invalidProductName)
                    return BadRequest("Invalid product name.");

                existingProduct.Name = submittedProduct.Name;
            }

            var newPrice = submittedProduct.Price != null;

            if (newPrice)
                existingProduct.Price = submittedProduct.Price;

            if (existingProduct.Save())
                return CreatedAtRoute("GetProductById", new { id = existingProduct.Id }, existingProduct);

            return BadRequest("Product could not be saved.");
        }
    }
}