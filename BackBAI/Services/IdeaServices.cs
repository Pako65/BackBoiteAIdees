using BackBAI.Models;
using BackBAI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var ideaToUpdate = _context.Idea.Include(i => i.IdeaGetCategory).FirstOrDefault(i => i.Id == id);

            if (ideaToUpdate == null)
                return false;
            
            if (ideaDTO.Title != null)
                ideaToUpdate.Title = ideaDTO.Title;

            if (ideaDTO.Description != null)
                ideaToUpdate.Description = ideaDTO.Description;

            if (ideaDTO.FkUsersId > 0)
                ideaToUpdate.FkUsersId = ideaDTO.FkUsersId;
            
            ideaToUpdate.UpdatedAt = DateTime.Now;

            if (ideaDTO.IdeaGetCategory != null && ideaDTO.IdeaGetCategory.Count > 0)
            {
                var newCategoryId = ideaDTO.IdeaGetCategory[0].CategoryId;
                var ideaCategory = ideaToUpdate.IdeaGetCategory.FirstOrDefault();
                if (ideaCategory != null)
                    _context.IdeaGetCategory.Remove(ideaCategory);

                ideaToUpdate.IdeaGetCategory.Add(new IdeaGetCategory { CategoryId = newCategoryId });
            }

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