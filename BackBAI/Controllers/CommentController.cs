using BackBAI.Models;
using BackBAI.Models.DTO;
using BackBAI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackBAI.Controllers
{
    [ApiController]
    [Route("Comments")]
    public class CommentController : ControllerBase
    {
        private readonly ideeContext _context;
        private readonly CommentServices _commentsServices;

        public CommentController(ideeContext context, CommentServices commentsServices)
        {
            _context = context;
            _commentsServices = commentsServices;
        }
        [HttpGet("GetComments")]
        public IActionResult GetComments()
        {
            var comments = _commentsServices.GetComments();
            return Ok(comments);
        }
        [HttpGet("{ideaId}/GetByIdeaId")]
        public ActionResult<IEnumerable<CommentsDTO>> GetCommentsByIdeaId(int ideaId)
        {
            var comments = _context.Comment
                .Where(comment => comment.FkIdeaId == ideaId)
                .Select(comment => new CommentsDTO
                {
                    Text = comment.Text,
                    CreatedAt = comment.CreatedAt,
                    UserId = comment.FkUsersIdComment,
                    IdeaId = comment.FkIdeaId
                })
                .ToList();

            return Ok(comments);
        }

        [HttpPost("CreateNewComments")]
        public IActionResult CreateComment([FromBody] CommentsDTO commentsDTO)
        {
            var idea = _context.Idea.Find(commentsDTO.IdeaId);

            if (idea == null)
                return BadRequest("Idea not found");

            var newComments = new Comment
            {
                Text = commentsDTO.Text,
                FkUsersIdComment = commentsDTO.UserId,
                FkIdeaId = commentsDTO.IdeaId,
            };

            _commentsServices.CreateComments(newComments);
            return Ok("new comment created");
        }
        [HttpPut("{id}/ModifyComments")]
        public IActionResult PutComments(int id, [FromBody] CommentsDTO commentsDTO)
        {
            var resultPut = _commentsServices.PutIdea(id, commentsDTO);

            if (!resultPut)
                return NotFound();

            return Ok(resultPut);
        }
        [HttpDelete("{id}/DeleteComments")]
        public IActionResult DeleteComment(int id)
        {
            var result = _commentsServices.DeleteCommentsById(id);

            if (result == false)
                return NotFound();

            return Ok(result);
        }

    }
}
