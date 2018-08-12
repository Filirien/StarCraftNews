namespace StarCraftNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class News
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        //public List<Comment> Comments { get; set; } = new List<Comment>();

        //public List<NewsVote> Votes { get; set; } = new List<NewsVote>();
    }
}
