using BackBAI.Models;
using BackBAI.Models.DTO;
using BackBAI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace BackBAI.Controllers
{
    [ApiController]
    [Route("Idea")]

    public class IdeaController : ControllerBase
    {
        private ideeContext _context;
        private IdeaServices _ideaServices;

        public IdeaController(ideeContext context, IdeaServices ideaServices)
        {
            _context = context;
            _ideaServices = ideaServices;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<IdeaWithCategoryDTO>> GetIdeasWithCategories()
        {
            var ideasWithCategories = _context.Idea
                .Join(
                    _context.Users, 
                    idea => idea.FkUsersId, 
                    user => user.Id, 
                    (idea, user) => new IdeaWithCategoryDTO
                    {
                        IdeaId = idea.Id,
                        Title = idea.Title,
                        Description = idea.Description,
                        CategoryId = idea.IdeaGetCategory != null && idea.IdeaGetCategory.Any() ? idea.IdeaGetCategory.First().Category.Id : 0,
                        CategoryName = idea.IdeaGetCategory != null && idea.IdeaGetCategory.Any() ? idea.IdeaGetCategory.First().Category.Name : "Pas de catégorie",
                        OwnerEmail = user.Email,
                        IsLiked = _context.Likes.Any(like => like.IdeaId == idea.Id && like.UsersId == user.Id),
                    })
                .ToList();

            return Ok(ideasWithCategories);
        }


        [HttpGet("{id}/getIdeaById")]
        public ActionResult<IEnumerable<IdeaWithCategoryDTO>> GetIdeaById(int id)
        {
            var ideaCategory = _context.Idea
                .Where(idea => idea.Id == id)
                .Select(idea => new IdeaWithCategoryDTO
                 {
                    IdeaId= idea.Id,
                    Title = idea.Title,
                    Description = idea.Description,
                    CategoryId = idea.IdeaGetCategory != null && idea.IdeaGetCategory.Any() ? idea.IdeaGetCategory.First().Category.Id : 0,
                    CategoryName = idea.IdeaGetCategory != null && idea.IdeaGetCategory.Any() ? idea.IdeaGetCategory.First().Category.Name : "Pas de catégorie"
                })
                .ToList();
            return Ok(ideaCategory);
        }

        [HttpPost("PostIdea")]
        public IActionResult CreateIdea([FromBody]IdeaDTO IdeaDTO)
        {
            var newIdea = new Idea
            {
                Title = IdeaDTO.Title,
                Description = IdeaDTO.Description,
                FkUsersId = IdeaDTO.FkUsersId,
            };

            if (IdeaDTO.IdeaGetCategory != null && IdeaDTO.IdeaGetCategory.Any())
            newIdea.IdeaGetCategory = IdeaDTO.IdeaGetCategory.Select(categoryDTO => new IdeaGetCategory { CategoryId = categoryDTO.CategoryId, Idea = newIdea }).ToList();

            _ideaServices.CreateIdea(newIdea);
            return Ok("Idea created youpi");
        }

        [HttpDelete("{id}/DeleteIdeaById")]

        public IActionResult DeleteBoiteAideesById(int id)
        {
            var resultDeleteById = _ideaServices.DeleteIdeaAndLikes(id);

            if (resultDeleteById == false)
            return NotFound();
            return Ok(resultDeleteById);
        }

        [HttpPut("{id}/PutIdea")]

        public IActionResult PutBoiteAidees(int id, [FromBody] IdeaDTO ideaDTO)
        {
            var resultPutById = _ideaServices.PutIdea(id, ideaDTO);

            if (!resultPutById)
            return NotFound();

            return Ok(resultPutById);
        }

    }

}
