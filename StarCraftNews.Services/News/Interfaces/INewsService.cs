namespace StarCraftNews.Services.News.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using StarCraftNews.Common.News.BindingModels;

    public interface INewsService
    {
        Task<IEnumerable<NewsListingBindingViewModel>> AllAsync(int page = 1);

        Task CreateAsync(string userId, string title, string description, string imageUrl);

        Task<NewsWithCommentsBindingModel> WithCommentsById(int id);

        Task Edit(int id, string title, string description, string imageUrl);

        Task<bool> Delete(int id);

        Task<int> AddOrUpdateVote(int newsId, string userId, int value);

        Task<int> Votes(int newsId);

        Task<IEnumerable<NewsTopThreeBindingModel>> TopThree();

        Task<NewsMinifiedBindingModel> ById(int id);

        Task<string> AuthorId(int id);
    }
}
