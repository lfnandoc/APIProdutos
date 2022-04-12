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

        [HttpGet(Name = "GetCategories")]
        public IActionResult Get()
        {
            return Ok(new Category().GetAllMapped().OfType<Category>());
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public IActionResult Get(int id)
        {
            var category = new Category().GetById(id) as Category;

            if (category != null)
                return Ok(category);

            return NotFound();
        }

        [HttpPost(Name = "AddCategory")]
        public IActionResult Post(CategoryPayload category)
        {
            var invalidCategoryName = category.Name == null || string.IsNullOrWhiteSpace(category.Name);

            if (invalidCategoryName)
                return BadRequest("Invalid category name.");

            var categoryEntity = new Category()
            {
                Name = category.Name
            };

            if (categoryEntity.Save())
                return CreatedAtRoute("GetCategoryById", new { id = categoryEntity.Id }, categoryEntity);

            return BadRequest("Category could not be saved.");
        }

        [HttpPut("{id}", Name = "UpdateCategory")]
        public IActionResult Put(int id, CategoryPayload submittedCategory)
        {
            var existingCategory = new Category().GetById(id) as Category;

            if (existingCategory == null)
                return NotFound();

            var newCategoryName = submittedCategory.Name != null;
            
            if (newCategoryName)
            {
                var invalidCategoryName = string.IsNullOrWhiteSpace(submittedCategory.Name);
                if (invalidCategoryName)
                    return BadRequest("Invalid category name.");

                existingCategory.Name = submittedCategory.Name;
            }

            if (existingCategory.Save())
                return CreatedAtRoute("GetCategoryById", new { id = existingCategory.Id }, existingCategory);

            return BadRequest("Category could not be saved.");
        }

        [HttpDelete("{id}", Name = "DeleteCategory")]
        public IActionResult Delete(int id)
        {
            var Category = new Category().GetById(id) as Category;

            if (Category == null)
                return NotFound();

            if (Category.HasAssociatedProducts())
                return BadRequest("Category could not be deleted because it has products associated with it.");           

            if (Category.Delete())
                return NoContent();

            return BadRequest("Category could not be deleted.");
        }
    }
}