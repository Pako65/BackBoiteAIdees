using BackBAI.Models;
using BackBAI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackBAI.Services
{
    public class IdeaServices : Controller
    { 
        // Modifie les messages de retour, au lieu de bool mettre string ou un object
        // c'est plus propre
        private readonly ideeContext _context;

        public IdeaServices(ideeContext context)
        {
            _context = context;
        }

        // GET: BoiteAideesServices
        public IEnumerable<Idea> Get()
        {
            var result = _context.Idea.ToList();
            return result;
        }
        // GET : Idea by id
        public Idea? GetIdeaById(int id)
        {
            var resultById = _context.Idea.FirstOrDefault(b => b.Id == id);
            if (resultById == null)
            {
                return null;
            }
            return resultById;
        }
        // POST : Create a new idea
        public Idea CreateIdea(Idea idea)
        {
            // Vous pouvez ajouter ici la logique de création de l'idée, par exemple :
            _context.Idea.Add(idea);
            _context.SaveChanges();

            return idea;
        }
        // PUT : Modify a idea
        public bool PutIdea(int id, IdeaDTO ideaDTO)
        {
            var postBoiteAidees = _context.Idea.Find(id);

            if (postBoiteAidees == null)
            {
                return false;
            }

            if (postBoiteAidees.Title != null)
                postBoiteAidees.Title = ideaDTO.Title;

            if (postBoiteAidees.Description != null)
                postBoiteAidees.Description = ideaDTO.Description;

            postBoiteAidees.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return true;

        }
        // DELETE : Delete a Idea by id
        public bool DeleteIdeaAndLikes(int id)
        {
            var idea = _context.Idea.Find(id);

            if (idea == null)
            {
                return false;
            }
            _context.Idea.Remove(idea);
            _context.SaveChanges();
            return true;
        }
    }
}