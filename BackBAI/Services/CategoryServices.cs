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
        //POST : New Category
        public Category CreateCategory(Category category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();

            return category;
        }
    }
}
