using Microsoft.AspNetCore.Mvc;
using BackBAI.Models;

namespace BackBAI.Services
{
    public class CategoryServices : Controller
    {
        private readonly ideeContext _context;

        public CategoryServices(ideeContext context)
        {
            _context = context; 
        }

        // GET : Category
        public IEnumerable<Category> GetCategory()
        {
            var category = _context.Category.ToList();
            return category;
        }
    }
}
