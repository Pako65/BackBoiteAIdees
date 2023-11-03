using BackBAI.Models;
using BackBAI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("categories/{ideaId}")]
        public IActionResult GetCategoriesForIdea(int ideaId)
        {
            var categories = _context.IdeaGetCategory
                .Where(igc => igc.IdeaId == ideaId)
                .Select(igc => igc.Category)
                .ToList();

            return Ok(categories);
        }
    }
}
