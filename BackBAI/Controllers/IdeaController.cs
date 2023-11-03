using BackBAI.Models;
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
        public IActionResult GetIdees()
        {
            var result = _ideaServices.Get();
            return Ok(result);
        }

        [HttpGet("{id}/getIdeaById")]
        public IActionResult GetIdeaById(int id)
        {
            var resultById = _ideaServices.GetIdeaById(id);
            if (resultById == null)
            {
                return NotFound();
            }
            return Ok(resultById);
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

            _ideaServices.CreateIdea(newIdea);
            return Ok("Idea created youpi");
        }

        [HttpDelete("{id}/DeleteIdeaById")]

        public IActionResult DeleteBoiteAideesById(int id)
        {
            var resultDeleteById = _ideaServices.DeleteIdeaAndLikes(id);

            if (resultDeleteById == false)
            {
                return NotFound();
            }
            return Ok(resultDeleteById);
        }

        [HttpPut("{id}/PutIdea")]

        public IActionResult PutBoiteAidees(int id, [FromBody] Idea idea)
        {
            var resultPutById = _ideaServices.PutIdea(id, idea);

            if (!resultPutById)
            {
                return NotFound();
            }

            return Ok(resultPutById);
        }

    }

}
