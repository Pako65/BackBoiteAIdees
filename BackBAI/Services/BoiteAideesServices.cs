using BackBAI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackBAI.Services
{
    public class BoiteAideesServices : Controller
    {
        private readonly ideeContext _context;

        // Modifie les messages de retour, au lieu de bool mettre string ou un object
        // c'est plus propre

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
        public IEnumerable<Category> GetCategory()
        {
            var category = _context.Category.ToList();
            return category;
        }
        public IEnumerable<Users> GetUsers()
        {
            var users = _context.Users.ToList();
            return users;
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
        public Users CreateUsers(Users users)
        {
            _context.Users.Add(users);
            _context.SaveChanges();

            return users;
        }
        public Idea CreateIdea(Idea idea)
        {
            // Vous pouvez ajouter ici la logique de création de l'idée, par exemple :
            _context.Idea.Add(idea);
            _context.SaveChanges();

            return idea;
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
    }
}
