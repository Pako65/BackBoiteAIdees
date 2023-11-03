using Microsoft.AspNetCore.Mvc;
using BackBAI.Models;
using BackBAI.Models.DTO;

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
        //DELETE : Delete a Category
        public bool DeleteCategoryById(int id)
        {
            var category = _context.Category.Find(id);

            if(category == null)
            {
                return false;
            }

            _context.Category.Remove(category);
            _context.SaveChanges();
            return true;
        }
        //PUT : Modify a Category
        public bool PutCategory(int id, CategoryDTO categoryDTO)
        {
            var result = _context.Category.Find(id);

            if(result == null)
            {
                return false;
            }

            if (result.Name != null)
                result.Name = categoryDTO.Name;

            _context.SaveChanges();

            return true;
        }
    }
}
