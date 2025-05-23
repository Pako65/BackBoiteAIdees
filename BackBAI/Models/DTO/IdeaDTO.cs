﻿namespace BackBAI.Models.DTO
{
    public class IdeaDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int FkUsersId { get; set; }
        public List<IdeaCategoryDTO> IdeaGetCategory { get; set; }
    }

    public class IdeaCategoryDTO
    {
        public int CategoryId { get; set; }
    }
}