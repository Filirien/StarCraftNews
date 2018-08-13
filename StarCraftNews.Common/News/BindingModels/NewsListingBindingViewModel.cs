namespace StarCraftNews.Common.News.BindingModels
{
    using AutoMapper;
    using System;
    using System.ComponentModel.DataAnnotations;
    using StarCraftNews.Data.Models;
    using System.Linq;
    using StarCraftNews.Common.Mapping;

    public class NewsListingBindingViewModel: IMapFrom<News>, IHaveCustomMapping
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public DateTime CreateOn { get; set; }

        public int Votes { get; set; }

        public int Comments { get; set; }

        public void ConfigureMapping(Profile mapper)
                 => mapper
                       .CreateMap<News, NewsListingBindingViewModel>()
                       .ForMember(n => n.Comments, cfg => cfg.MapFrom(nn => nn.Comments.Count()))
                       .ForMember(n => n.Votes, cfg => cfg.MapFrom(nn => nn.Votes.Sum(v => v.Value)));
    }
}
