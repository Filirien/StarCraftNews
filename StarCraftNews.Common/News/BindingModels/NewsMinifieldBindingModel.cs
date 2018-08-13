namespace StarCraftNews.Common.News.BindingModels
{
    using StarCraftNews.Common.Mapping;
    using StarCraftNews.Data.Models;

    public class NewsMinifieldBindingModel : IMapFrom<News>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
