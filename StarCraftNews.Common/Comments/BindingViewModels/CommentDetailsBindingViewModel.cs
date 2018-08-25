namespace StarCraftNews.Common.Comments.BindingViewModels
{
    using System;
    using System.Collections.Generic;

    public class CommentDetailsBindingViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }
        
        public List<CommentChildBindingViewModel> ChildrenComments { get; set; }
        
    }
}
