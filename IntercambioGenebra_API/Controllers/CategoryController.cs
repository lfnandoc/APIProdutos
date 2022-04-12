using IntercambioGenebraAPI.Entities;
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
        public IEnumerable<Category> Get()
        {
            return new Category().GetAllMapped().OfType<Category>();
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public Category? Get(int id)
        {
            return new Category().GetById(id) as Category;
        }

        [HttpPost(Name = "AddCategory")]
        public IActionResult Post(Category Category)
        {
            var userSubmittedId = Category.Id != -2;

            if (userSubmittedId)
                return BadRequest("Custom Category ids are not allowed.");

            var invalidCategoryName = Category.Name == null || string.IsNullOrWhiteSpace(Category.Name);

            if (invalidCategoryName)
                return BadRequest("Invalid Category name.");         

            if (Category.Save())
                return CreatedAtRoute("GetCategoryById", new { id = Category.Id }, Category);

            return BadRequest("Category could not be saved.");
        }

        [HttpPut("{id}", Name = "UpdateCategory")]
        public IActionResult Put(int id, Category submittedCategory)
        {
            var existingCategory = new Category().GetById(id) as Category;

            if (existingCategory == null)
                return NotFound();

            var userSubmittedId = submittedCategory.Id != -2;

            if (userSubmittedId)
                return BadRequest("Updating Category ids is not allowed.");

            var newCategoryName = submittedCategory.Name != null;
            
            if (newCategoryName)
            {
                var invalidCategoryName = string.IsNullOrWhiteSpace(submittedCategory.Name);
                if (invalidCategoryName)
                    return BadRequest("Invalid Category name.");

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

            if (Category.Delete())
                return NoContent();

            return BadRequest("Category could not be deleted.");
        }
    }
}