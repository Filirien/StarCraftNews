namespace StarCraftNews.Services.News
{
    using StarCraftNews.Common.News.BindingModels;
    using StarCraftNews.Data;
    using StarCraftNews.Services.News.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class NewsService : INewsService
    {
        private readonly StarCraftNewsDbContext db;

        public NewsService(StarCraftNewsDbContext db)
        {
            this.db = db;
        }
        public Task<int> AddOrUpdateVote(int newsId, string userId, int value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NewsListingBindingViewModel>> AllAsync(int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<string> AuthorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<NewsMinifieldBindingModel> ById(int id)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(string userId, string title, string imageurl)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NewsTopThreeBindingModel>> TopThree()
        {
            throw new NotImplementedException();
        }

        public Task<int> Votes(int newsId)
        {
            throw new NotImplementedException();
        }

        public Task<NewsWithCommentsBindingModel> WithCommentsById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
