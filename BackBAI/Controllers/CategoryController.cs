using BackBAI.Models;
using BackBAI.Models.DTO;
using BackBAI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BackBAI.Controllers
{
    [ApiController]
    [Route("Category")]
    public class CategoryController : Controller
    {
        private ideeContext _context;
        private CategoryServices _categoryServices;

        public CategoryController(ideeContext context, CategoryServices categoryServices)
        {
            _context = context;
            _categoryServices = categoryServices;
        }

        [HttpGet("GetAllComment")]
        public IActionResult GetCategory()
        {
            var category = _categoryServices.GetCategory();
            return Ok(category);
        }

        [HttpGet("categoriy/{ideaId}")]
        public IActionResult GetCategoriesForIdea(int ideaId)
        {
            var categories = _context.IdeaGetCategory
                .Where(igc => igc.IdeaId == ideaId)
                .Select(igc => igc.Category)
                .ToList();

            return Ok(categories);
        }
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDTO categoryDto)
        {
            var newCategory = new Category
            {
                Name = categoryDto.Name,
            };

            _categoryServices.CreateCategory(newCategory);
            return Ok("New category created");
        }
        [HttpDelete("{id}/DeleteCategory")]
        public IActionResult DeleteCategory(int id)
        {
            var result = _categoryServices.DeleteCategoryById(id);

            if (result == false)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPut("{id}/PutCategory")]
        public IActionResult PutCategory(int id, [FromBody] CategoryDTO categoryDto)
        {
            var result = _categoryServices.PutCategory(id, categoryDto);

            if(!result)
            {
                return NotFound();
            }

            return Ok(result);
        }

    }
}
