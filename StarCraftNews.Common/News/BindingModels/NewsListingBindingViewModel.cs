namespace StarCraftNews.Common.News.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class NewsListingBindingViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Votes { get; set; }

        public int Comments { get; set; }
    }
}
