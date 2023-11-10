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
        private readonly ideeContext _context;
        private readonly LikesServices _likesServices;

        public LikesController(ideeContext context, LikesServices likesServices)
        {
            _context = context;
            _likesServices = likesServices;
        }
        [HttpGet("likecount/{ideaId}")]
        public IActionResult GetLikeCountForIdea(int ideaId)
        {
            try
            {
                int likeCount = _likesServices.GetTotalLikesForIdea(ideaId);
                return Ok(likeCount);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erreur : {ex.Message}");
            }
        }
        [HttpGet("GetUserLikes")]
        public IActionResult GetUserLikes(int userId)
        {
            try
            {
                var userLikes = _context.Likes.Where(l => l.UsersId == userId).Select(l => l.IdeaId).ToList();

                return Ok(userLikes);
            } catch (Exception)
            {
                return StatusCode(500, $"Erreur lors de la récupération des likes par users");
            }
        }

        [HttpGet("GetAllLikes")]
        public IActionResult GetLikes()
        {
            var likes = _likesServices.GetLikes();
            return Ok(likes);
        }

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
