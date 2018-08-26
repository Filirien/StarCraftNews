namespace StarCraftNews.Web.Infrastructure.Mapping
{
    using AutoMapper;
    using StarCraftNews.Common.Comments.BindingViewModels;
    using StarCraftNews.Common.News.BindingModels;
    using StarCraftNews.Data.Models;
    using StarCraftNews.Services.Admin.Models;
    using System.Linq;

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, AdminUserListingServiceModel>();

            CreateMap<News, NewsListingBindingViewModel>()
                       .ForMember(n => n.Comments, cfg => cfg.MapFrom(nn => nn.Comments.Count()))
                       .ForMember(n => n.Votes, cfg => cfg.MapFrom(nn => nn.Votes.Sum(v => v.Value)));

            CreateMap<News, NewsMinifiedBindingModel>();

           CreateMap<News, NewsWithCommentsBindingModel>()
                     .ForMember(nn => nn.Author, cfg => cfg.MapFrom(n => n.Author.UserName))
                     .ForMember(nn => nn.Votes, cfg => cfg.MapFrom(n => n.Votes.Sum(v => v.Value)));

            CreateMap<Comment, CommentChildBindingViewModel>()
                .ForMember(cm => cm.Author, cfg => cfg.MapFrom(c => c.Author.UserName));

            CreateMap<News, CommentDetailsBindingViewModel>()
                .ForMember(cm => cm.Author, cfg => cfg.MapFrom(c => c.Author.UserName));

            CreateMap<User, AdminUserListingServiceModel>();
            //var allTypes = AppDomain
            //    .CurrentDomain
            //    .GetAssemblies()
            //    .Where(a => a.GetName().Name.Contains("StarCraftNews") && !a.GetName().Name.Contains("Web"))
            //    .SelectMany(a => a.GetTypes());

            //allTypes
            //    .Where(t => t.IsClass && !t.IsAbstract && t
            //        .GetInterfaces()
            //        .Where(i => i.IsGenericType)
            //        .Select(i => i.GetGenericTypeDefinition())
            //        .Contains(typeof(IMapFrom<>)))
            //    .Select(t => new
            //    {
            //        Destination = t,
            //        Source = t
            //            .GetInterfaces()
            //            .Where(i => i.IsGenericType)
            //            .Select(i => new
            //            {
            //                Definition = i.GetGenericTypeDefinition(),
            //                Arguments = i.GetGenericArguments()
            //            })
            //            .Where(i => i.Definition == typeof(IMapFrom<>))
            //            .SelectMany(i => i.Arguments)
            //            .First(),
            //    })
            //    .ToList()
            //    .ForEach(mapping => this.CreateMap(mapping.Source, mapping.Destination));

            //allTypes
            //    .Where(t => t.IsClass
            //        && !t.IsAbstract
            //        && typeof(IHaveCustomMapping).IsAssignableFrom(t))
            //    .Select(Activator.CreateInstance)
            //    .Cast<IHaveCustomMapping>()
            //    .ToList()
            //    .ForEach(mapping => mapping.ConfigureMapping(this));
        }
    }
}
