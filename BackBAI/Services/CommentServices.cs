using BackBAI.Models;
using BackBAI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackBAI.Services
{
    public class CommentServices : Controller
    {
        private readonly ideeContext _context;

        public CommentServices(ideeContext context)
        {
            _context = context;
        }

        // GET : Comments
        public IEnumerable<Comment> GetComments()
        {
            var comments = _context.Comment.ToList();
            return comments;
        }

        // GET : getById
        public Comment? GetCommentById(int id)
        {
            var result = _context.Comment.FirstOrDefault(c => c.Id == id);

            if (result == null)
                return null;

            return result;
        }
        // POST : new Comments
        public Comment CreateComments(Comment comment)
        {
            _context.Comment.Add(comment);
            _context.SaveChanges();
            return comment;
        }
        // PUT : Modify a likes
        public bool PutIdea(int id, CommentsDTO commentsDTO)
        {
            var result = _context.Comment.Find(id);

            if (result == null)
            {
                return false;
            }

            if (commentsDTO.Text != null)
            {
                result.Text = commentsDTO.Text;
            }

            result.UpdatedAt = DateTime.Now;

            _context.SaveChanges();

            return true;
        }
        //DELETE : Delete a comments
        public bool DeleteCommentsById(int id)
        {
            var result = _context.Comment.Find(id);

            if (result == null)
                return false;

            _context.Comment.Remove(result);
            _context.SaveChanges();

            return true;
                
            
        }
    }
}
