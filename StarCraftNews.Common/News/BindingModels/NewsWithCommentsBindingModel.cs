namespace StarCraftNews.Common.News.BindingModels
{
    using StarCraftNews.Common.Comments.BindingViewModels;
    using System;
    using System.Collections.Generic;

    public class NewsWithCommentsBindingModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public int Votes { get; set; }

        public List<CommentDetailsBindingViewModel> Comments { get; set; }
        
    }
}
