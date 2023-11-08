using BackBAI.Models;
using BackBAI.Models.DTO;
using BackBAI.Services;
using Microsoft.AspNetCore.Mvc;


namespace BackBAI.Controllers
{
    [ApiController]
    [Route("Likes")]
    public class LikesController : ControllerBase
    {
        private ideeContext _context;
        private LikesServices _likesServices;

        public LikesController(ideeContext context, LikesServices likesServices)
        {
            _context = context;
            _likesServices = likesServices;
        }

        //[HttpGet("GetAllLikes")]
        //public IActionResult GetLikes()
        //{
        //    var likes = _likesServices.GetLikes();
        //    return Ok(likes);
        //}

        [HttpPost("PostNewLikes")]
        public async Task<IActionResult> AddLikes(int userId, int ideaId)
        {
            if (await _likesServices.AddLikes(userId, ideaId))
            {
                return Ok("Likes bien ajouté dans la base");
            }

            return BadRequest("L'utilisateur a déjà aimé cette idée");
        }

        [HttpDelete("DeleteLikesById")]
        public async Task<IActionResult> DeleteLikes(int userId, int ideaId)
        {
            if (await _likesServices.DeleteLikes(userId, ideaId))
                return Ok("ca marche");

            return BadRequest("ca marche pas");
        }
    }
}
