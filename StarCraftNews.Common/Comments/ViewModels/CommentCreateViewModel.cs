namespace StarCraftNews.Common.Comments.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    using static StarCraftNews.Data.DataConstants;

    public class CommentCreateViewModel
    {
        [Required]
        [StringLength(CommentContentMaxLength, MinimumLength = CommentContentMinLength)]
        public string Content { get; set; }

        public int? NewsId { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
