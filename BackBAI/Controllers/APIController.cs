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

        [HttpGet("GetAll")]
        public IActionResult GetIdees()
        {
            var result = _boiteAideesServices.Get();
            return Ok(result);
        }
        [HttpGet("GetAllComment")]
        public IActionResult GetCategory()
        {
            var category = _boiteAideesServices.GetCategory();
            return Ok(category);
        }

        [HttpPost("PostIdea")]
        public IActionResult CreateIdea([FromBody]IdeaPresenter ideaPresenter)
        {
            var newIdea = new Idea
            {
                Title = ideaPresenter.Title,
                Description = ideaPresenter.Description,
                FkUsersId = ideaPresenter.FkUsersId,
            };

            if (ideaPresenter.IdeaGetCategory != null && ideaPresenter.IdeaGetCategory.Any())
            {
                newIdea.IdeaGetCategory = ideaPresenter.IdeaGetCategory
                    .Select(categoryDTO => new IdeaGetCategory
                    {
                        CategoryId = categoryDTO.CategoryId,
                        Idea = newIdea
                    })
                    .ToList();
            }

            _boiteAideesServices.CreateIdea(newIdea);
            return Ok("Idea created youpi");
        }

        //[HttpPost("postIdea")]
        //public async Task<IActionResult> CreateIdea([FromBody] Idea idea)
        //{
        //    if (idea == null)
        //    {
        //        return BadRequest("Les données de l'idée sont invalides.");
        //    }

        //    int ideaId = await _boiteAideesServices.CreateIdeaAsync(idea);

        //    // Assurez-vous que le nom de l'action "GetIdea" correspond à l'action de récupération de l'idée par ID.
        //    // Vous devrez peut-être ajuster le nom de l'action en fonction de votre propre routage.
        //    return CreatedAtAction("GetIdeaById", new { id = ideaId }, idea);
        //}


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
//    "Title": "Nouvelle idée",
//  "FkUsersId": 1,
//  "IdeaGetCategory": [
//    {
//        "CategoryId": 1
//    }
//  ],
//  "Description": "Description de la nouvelle idée"
//}