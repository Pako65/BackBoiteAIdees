using BackBAI.Models;
using BackBAI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

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
        [HttpGet("GetAllUsers")]
        public IActionResult GetUsers()
        {
            var users = _boiteAideesServices.GetUsers();
            return Ok(users);
        }
        // ajoute un nouvel utilisateurs dans la base avec le JSON envoyé depuis supabase
        [HttpPost("CreateNewUsers")]
        public IActionResult Post(WebhookDto webhookDto)
        {
            // Accédez aux propriétés email et encrypted_password de l'objet Record

            string email = webhookDto.record?.email;
            string encryptedPassword = webhookDto.record?.encrypted_password;

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(encryptedPassword))
            {
                var user = new Users
                {
                    Email = email,
                    Password = encryptedPassword
                };

                _context.Users.Add(user);
                _context.SaveChanges(); // Enregistrez les changements en base de données
            }


            return Ok("Données email et encrypted_password ajoutées en base de données avec succès.");
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