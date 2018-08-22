namespace StarCraftNews.Common.Comments.BindingViewModels
{
    using AutoMapper;
    using StarCraftNews.Common.Mapping;
    using StarCraftNews.Data.Models;
    using System;

    public class CommentChildBindingViewModel : IMapFrom<Comment>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Comment, CommentChildBindingViewModel>()
                .ForMember(cm => cm.Author, cfg => cfg.MapFrom(c => c.Author.UserName));
    }
}
