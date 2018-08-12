using System;
using System.Collections.Generic;
using System.Text;

namespace StarCraftNews.Data.Models
{
    public class NewsVote
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int NewsId { get; set; }

        public News News { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
