namespace StarCraftNews.Common.Comments.BindingViewModels
{
    using System;

    public class CommentChildBindingViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }
        
    }
}
