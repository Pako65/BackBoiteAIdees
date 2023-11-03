namespace BackBAI.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte Admin { get; set; }

        public List<CommentDTO> Comments { get; set; }
        public List<IdeaDTOs> Ideas { get; set; }
        public List<LikesDTO> Likes { get; set; }
    }

    public class CommentDTO
    {
        public int CommentId { get; set; }
    }

    public class IdeaDTOs
    {
        public int IdeaId { get; set; }
    }

    public class LikesDTO
    {
        public int LikesId { get; set; }
    }
}
