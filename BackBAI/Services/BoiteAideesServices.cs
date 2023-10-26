using BackBAI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackBAI.Services
{
    public class BoiteAideesServices : Controller
    {
        private readonly ideeContext _context;

        public BoiteAideesServices(ideeContext context)
        {
            _context = context;
        }
        // GET: BoiteAideesServices
        public IEnumerable<Idea> Get()
        {
            var result = _context.Idea.ToList();
            return result;
        }
        public Idea? GetIdeaById(int id)
        {
            var resultById = _context.Idea.FirstOrDefault(b => b.Id == id);
            if (resultById == null)
            {
                return null;
            }
            return resultById;
        }
        public bool Put(int id, Idea idea)
        {
            var postBoiteAidees = _context.Idea.Find(id);

            if (postBoiteAidees == null)
            {
                return false;
            }

            if (postBoiteAidees.Title != null)
                postBoiteAidees.Title = idea.Title;
            
            if (postBoiteAidees.Description != null)
                postBoiteAidees.Description = idea.Description;
        

            _context.SaveChanges();

            return true;

        }
        public bool DeleteIdeaAndLikes(int ideaId)
        {
            var idea = _context.Idea.Find(ideaId);

            if (idea == null)
            {
                // L'idée n'existe pas, retournez false ou gérez l'erreur.
                return false;
            }
            // Supprimez ensuite l'idée elle-même.
            _context.Idea.Remove(idea);

            // Enregistrez les modifications dans la base de données.
            _context.SaveChanges();

            return true;
        }

        //public bool DeleteById(int id)
        //{
        //    var idea = _context.Idea.Find(id);

        //    if (idea == null)
        //    {
        //        return false;
        //    }

        //    var commentsToDelete = _context.Comment.Where(c => c.FkIdeaId == id);
        //    var likesToDelete = _context.Likes.Where(c => c.FkIdeaIdLikes == id);

        //    _context.Comment.RemoveRange(commentsToDelete);
        //    _context.Likes.RemoveRange(likesToDelete);
        //    _context.SaveChanges();

        //    _context.Idea.Remove(idea);
        //    _context.SaveChanges();

        //    return true;
        //}



        //// GET: BoiteAideesServices/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: BoiteAideesServices/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: BoiteAideesServices/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: BoiteAideesServices/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: BoiteAideesServices/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: BoiteAideesServices/Delete/5


        //// POST: BoiteAideesServices/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
