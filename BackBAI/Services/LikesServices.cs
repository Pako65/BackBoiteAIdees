using Microsoft.AspNetCore.Mvc;
using BackBAI.Models;
using BackBAI.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackBAI.Services
{
    public class LikesServices : Controller
    {
        private readonly ideeContext _context;

        public LikesServices(ideeContext context)
        {
            _context = context;
        }

        //GET : Likes
        public IEnumerable<Likes> GetLikes()
        {
            var likes = _context.Likes.ToList();
            return likes;
        }
        //POST : New Likes
        public async Task<bool> AddLikes(int userId, int ideaId)
        {
            var existingLike = _context.Likes.Local.FirstOrDefault(l => l.UsersId == userId && l.IdeaId == ideaId);

            if (existingLike != null)
                return false;
            
            var newLike = new Likes
            {
                UsersId = userId,
                IdeaId = ideaId
            };

            _context.Likes.Add(newLike);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteLikes (int userId, int ideaId)
        {
            var likeToDelete = await _context.Likes.FirstOrDefaultAsync(a => a.UsersId == userId &&  a.IdeaId == ideaId);

            if (likeToDelete !=  null)
            {
                _context.Likes.Remove(likeToDelete);
                 await _context.SaveChangesAsync();
                return true;
            }

            return false;   
        }

        //FONCTION TotalLikes
        public int GetTotalLikesForIdea(int ideaId)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT dbo.total_likes(@ideaId) AS TotalLikes", connection))
                {
                    command.Parameters.Add(new SqlParameter("@ideaId", ideaId));

                    var result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return (int)result;
                    }
                }
            }

            return 0;
        }

    }
}


//PROCEDURE STOCKE A AJOUTE DANS LA BDD

//USE[boiteIdee]
//GO
///****** Object:  StoredProcedure [dbo].[TotalLikesById]    Script Date: 08/11/2023 10:30:59 ******/
//SET ANSI_NULLS ON
//GO
//SET QUOTED_IDENTIFIER ON
//GO
//ALTER PROCEDURE [dbo].[TotalLikesById]
//AS
//BEGIN
//    SELECT idea_id, COUNT(idea_id) AS TotalLikes
//    FROM likes
//    GROUP BY idea_id
//    HAVING COUNT(idea_id) > 0;
//END;


//SI JAMAIS SA MARCHE PAS -> 

//CREATE PROCEDURE TotalLikesById
//AS
//BEGIN
//    SELECT idea_id, COUNT(idea_id) AS TotalLikes
//    FROM likes
//    GROUP BY idea_id
//    HAVING COUNT(idea_id) > 0;
//END;