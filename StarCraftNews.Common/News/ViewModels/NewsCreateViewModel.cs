namespace StarCraftNews.Common.News.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class NewsCreateViewModel
    {
        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
