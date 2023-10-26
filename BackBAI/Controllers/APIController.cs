using BackBAI.Models;
using BackBAI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;

namespace BackBAI.Controllers
{
    [ApiController]
    [Route("controller")]

    public class ApiController : ControllerBase
    {
        private ideeContext _context;
        private BoiteAideesServices _boiteAideesServices;

        public ApiController(ideeContext context, BoiteAideesServices boiteAideesServices)
        {
            _context = context;
            _boiteAideesServices = boiteAideesServices;
        }

        [HttpGet(Name = "GetIdea")]
        public IActionResult GetIdees()
        {
            var result = _boiteAideesServices.Get();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult PostIdea([FromBody] Idea idea)
        {
            try
            {
                //idea.Id = 0; 
                idea.CreatedAt = DateTime.Now;
                _context.Idea.Add(idea);
                _context.SaveChanges();

                return Ok("L'idée a été ajoutée avec succès.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de l'ajout de l'idée : {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIdea(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // Supprime les likes associés à l'idée
                    _context.Database.ExecuteSqlRaw("DELETE FROM Likes WHERE FkIdeaIdLikes = {0}", id);

                    // Supprime les commentaires associés
                    _context.Comment.RemoveRange(_context.Comment.Where(c => c.FkIdeaId == id));

                    // Supprime l'idée
                    var idea = _context.Idea.Find(id);
                    if (idea != null)
                    {
                        _context.Idea.Remove(idea);
                    }

                    _context.SaveChanges();

                    transaction.Commit();

                    return NoContent(); // Retourne une réponse 204 (No Content) si la suppression est réussie.
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500); // Une erreur s'est produite lors de la suppression.
                }
            }
        }
        // faut savoir comment faire la table like, est ce que tu laisses la jointure bizarre, est ce que tu fais une table ?
        // car erreur sur méthode Delete, impossible de supprimer l'idée a cause des autres idea dans d'autre table (fk_idea...)


    }

}

// JSON POUR LE POST
//{
//    "title": "Mon idée",
//  "description": "Description de mon idée",
//  "createdAt": "2023-10-24T17:32:58.195Z",
//  "updatedAt": "2023-10-24T17:32:58.195Z",
//  "fkUsersId": 1, 
//  "comment": [
//    {
//        "text": "Commentaire sur mon idée",
//      "createdAt": "2023-10-24T17:32:58.195Z",
//      "updatedAt": "2023-10-24T17:32:58.195Z",
//      "fkUsersIdComment": 2
//    }
//  ],
//  "category": [
//    {
//        "name": "Nom de la catégorie"
//    }
//  ]
//}