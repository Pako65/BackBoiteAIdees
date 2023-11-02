namespace BackBAI.Models
{
    public class UserPresenter
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte Admin { get; set; }

        public List<CommentPresenter> Comments { get; set; }
        public List<IdeaPresente> Ideas { get; set; }
        public List<LikesPresenter> Likes { get; set; }
    }

    public class CommentPresenter
    {
        public int CommentId { get; set; }
    }

    public class IdeaPresente
    {
        public int IdeaId { get; set; }
    }

    public class LikesPresenter
    {
        public int LikesId { get; set; }
    }
}
