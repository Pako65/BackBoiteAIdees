namespace BackBAI.Models.DTO
{
    public class IdeaWithCategoryDTO
    {
        public int IdeaId { get; set; }
        public string ?Title { get; set; }
        public string ?Description { get; set; }
        public int CategoryId { get; set; }
        public string ?CategoryName { get; set; }
        public string? OwnerEmail { get; set; }
        public bool IsLiked { get; set; }
    }
}
