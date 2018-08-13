namespace StarCraftNews.Common.News.BindingModels
{
    using AutoMapper;
    using StarCraftNews.Common.Mapping;
    using StarCraftNews.Data.Models;
    using System;
    using System.Linq;

    public class NewsWithCommentsBindingModel : IMapFrom<News>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public int Votes { get; set; }

        //public Lsit<CommentDetailsBindingModel> Comments { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<News, NewsWithCommentsBindingModel>()
            .ForMember(nn => nn.Author, cfg => cfg.MapFrom(n => n.Author.UserName))
            .ForMember(nn => nn.Votes, cfg => cfg.MapFrom(n => n.Votes.Sum(v => v.Value)));
    }
}
