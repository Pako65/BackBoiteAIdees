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
        public IActionResult CreateIdea([FromBody]IdeaDTO IdeaDTO)
        {
            var newIdea = new Idea
            {
                Title = IdeaDTO.Title,
                Description = IdeaDTO.Description,
                FkUsersId = IdeaDTO.FkUsersId,
            };

            if (IdeaDTO.IdeaGetCategory != null && IdeaDTO.IdeaGetCategory.Any())
            {
                newIdea.IdeaGetCategory = IdeaDTO.IdeaGetCategory
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

        public IActionResult PutBoiteAidees(int id, [FromBody] IdeaDTO ideaDTO)
        {
            var resultPutById = _ideaServices.PutIdea(id, ideaDTO);

            if (!resultPutById)
            {
                return NotFound();
            }

            return Ok(resultPutById);
        }

    }

}
