namespace StarCraftNews.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public List<News> News { get; set; } = new List<News>();

        public List<Comment> Comments { get; set; } = new List<Comment>();

        public List<NewsVote> NewsVotes { get; set; } = new List<NewsVote>();

        public List<CommentVote> CommentVotes { get; set; } = new List<CommentVote>();
    }
}
