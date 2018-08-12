namespace StarCraftNews.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? NewsId { get; set; }

        public News News { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        public int? ParentCommentId { get; set; }

        public Comment ParentComment { get; set; }

        public List<Comment> ChildrenComments { get; set; } = new List<Comment>();

        public List<CommentVote> Votes { get; set; } = new List<CommentVote>();
    }
}
