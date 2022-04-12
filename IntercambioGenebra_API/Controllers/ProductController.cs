using IntercambioGenebraAPI.Entities;
using IntercambioGenebraAPI.Payloads;
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
        public IActionResult Get()
        {
            return Ok(new Product().GetAllMapped().OfType<Product>());
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult Get(int id)
        {
            var product = new Product().GetById(id) as Product;

            if (product != null)
                return Ok(product);

            return NotFound();
        }

        [HttpPost(Name = "AddProduct")]
        public IActionResult Post(ProductPayload product)
        {
            var invalidProductName = product.Name == null || string.IsNullOrWhiteSpace(product.Name);

            if (invalidProductName)
                return BadRequest("Invalid product name.");

            var invalidCategory = product.CategoryId == null || new Category().GetById((int) product.CategoryId) == null;

            if (invalidCategory)
                return BadRequest("Invalid category.");

            product.Price ??= 0;

            var productEntity = new Product() 
            { 
                Name = product.Name, 
                Price = product.Price, 
                CategoryId = product.CategoryId 
            };

            if (productEntity.Save())
                return CreatedAtRoute("GetProductById", new { id = productEntity.Id }, productEntity);

            return BadRequest("Product could not be saved.");
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        public IActionResult Put(int id, ProductPayload submittedProduct)
        {
            var existingProduct = new Product().GetById(id) as Product;

            if (existingProduct == null)
                return NotFound();

            var newProductName = submittedProduct.Name != null;
            
            if (newProductName)
            {
                var invalidProductName = string.IsNullOrWhiteSpace(submittedProduct.Name);
                if (invalidProductName)
                    return BadRequest("Invalid product name.");

                existingProduct.Name = submittedProduct.Name;
            }

            var newCategory = submittedProduct.CategoryId != null;
            if (newCategory)
            {
                var invalidCategory = new Category().GetById((int)submittedProduct.CategoryId) == null;
                if (invalidCategory)
                    return BadRequest("Invalid category.");

                existingProduct.CategoryId = submittedProduct.CategoryId;
            }

            var newPrice = submittedProduct.Price != null;

            if (newPrice)
                existingProduct.Price = submittedProduct.Price;

            if (existingProduct.Save())
                return CreatedAtRoute("GetProductById", new { id = existingProduct.Id }, existingProduct);

            return BadRequest("Product could not be saved.");
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        public IActionResult Delete(int id)
        {
            var product = new Product().GetById(id) as Product;

            if (product == null)
                return NotFound();

            if (product.Delete())
                return NoContent();

            return BadRequest("Product could not be deleted.");
        }
    }
}