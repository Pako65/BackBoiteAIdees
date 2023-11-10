namespace BackBAI.Models.DTO
{
    public class CommentsDTO
    {
        public string? Text { get; set; }
        //public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int IdeaId { get; set; }
    }

}
