using BackBAI.Models;
using BackBAI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;

namespace BackBAI.Controllers
{
    [ApiController]
    [Route("api")]

    public class ApiController : ControllerBase
    {
        private ideeContext _context;
        private BoiteAideesServices _boiteAideesServices;

        public ApiController(ideeContext context, BoiteAideesServices boiteAideesServices)
        {
            _context = context;
            _boiteAideesServices = boiteAideesServices;
        }

        [HttpGet("getAll")]
        public IActionResult GetIdees()
        {
            var result = _boiteAideesServices.Get();
            return Ok(result);
        }


        [HttpPost("postIdea")]
        public IActionResult PostIdea([FromBody] Idea idea)
        {
            try
            {
                //idea.Id = 0; 
                idea.CreatedAt = DateTime.Now;
                _context.Idea.Add(idea);
                _context.SaveChanges();

                return Ok("L'idée a été ajoutée avec succès.");
                // changez le OK en created, cela permet de retourner le created, ce qui est mieux
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur lors de l'ajout de l'idée : {ex.Message}");
            }
        }

        [HttpGet("{id}/getIdeaById")]
        public IActionResult GetIdeaById(int id)
        {
            var resultById = _boiteAideesServices.GetIdeaById(id);
            if (resultById == null)
            {
                return NotFound();
            }
            return Ok(resultById);
        }
        [HttpDelete("{id}/DeleteIdeaById")]

        public IActionResult DeleteBoiteAideesById(int id)
        {
            var resultDeleteById = _boiteAideesServices.DeleteIdeaAndLikes(id);

            if (resultDeleteById == false)
            {
                return NotFound();
            }
            return Ok(resultDeleteById);
        }

        [HttpPut("{id}/PutIdea")]

        public IActionResult PutBoiteAidees(int id, [FromBody] Idea idea)
        {
            var resultPutById = _boiteAideesServices.Put(id, idea);

            if (!resultPutById)
            {
                return NotFound();
            }

            return Ok(resultPutById);
        }

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