namespace StarCraftNews.Common.Comments.BindingViewModels
{
    using AutoMapper;
    using StarCraftNews.Common.Mapping;
    using StarCraftNews.Data.Models;
    using System;
    using System.Collections.Generic;

    public class CommentDetailsBindingViewModel : IMapFrom<Comment>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }


        public List<CommentChildBindingViewModel> ChildrenComments { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Comment, CommentDetailsBindingViewModel>()
                .ForMember(cm => cm.Author, cfg => cfg.MapFrom(c => c.Author.UserName));
    }
}
