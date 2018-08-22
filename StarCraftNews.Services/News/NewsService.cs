namespace StarCraftNews.Services.News
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using StarCraftNews.Common.News.BindingModels;
    using StarCraftNews.Data;
    using StarCraftNews.Data.Models;
    using StarCraftNews.Services.News.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class NewsService : INewsService
    {
        private readonly StarCraftNewsDbContext db;
        private readonly int NewsPageSize = 2;

        public NewsService(StarCraftNewsDbContext db)
        {
            this.db = db;
        }
        public async Task<int> AddOrUpdateVote(int newsId, string userId, int value)
        {
            if (!await this.DoesNewsExistAsync(newsId))
            {
                return -1;
            }

            var vote = this.db.NewsVotes.Where(n => n.NewsId == newsId && n.UserId == userId).FirstOrDefault();

            value = this.NormalizeValue(value);
            if (vote == null)
            {
                vote = new NewsVote
                {
                    Value = value,
                    NewsId = newsId,
                    UserId = userId,
                };
                this.db.Add(vote);
            }
            else
            {
                var difference = vote.Value + value;
                vote.Value = this.NormalizeValue(difference);
            }
            await this.db.SaveChangesAsync();

            return await this.Votes(newsId);
        }

        private async Task<bool> DoesNewsExistAsync(int newsId)
        {
            return await this.db.News.AnyAsync(n => n.Id == newsId);
        }

        public async Task<IEnumerable<NewsListingBindingViewModel>> AllAsync(int page = 1)
        {
            return await this.db
                 .News
                 .OrderByDescending(n => n.CreatedOn)
                 .Skip((page - 1) * NewsPageSize)
                 .Take(NewsPageSize)
                 .ProjectTo<NewsListingBindingViewModel>()
                 .ToListAsync();
        }

        public async Task<string> AuthorId(int id)
        {
            return await this.db.News.Where(n => n.Id == id).Select(n => n.AuthorId).FirstOrDefaultAsync();
        }

        public async Task<NewsMinifieldBindingModel> ById(int id)
        {
            return await this.db.News.Where(n => n.Id == id).ProjectTo<NewsMinifieldBindingModel>().FirstOrDefaultAsync();
        }

        public async Task CreateAsync(string userId, string title, string imageUrl)
        {
            News news = new News
            {
                Title = title,
                ImageUrl = imageUrl,
                AuthorId = userId,
                CreatedOn = DateTime.Now
            };

            this.db.Add(news);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(int id, string title, string imageUrl)
        {
            var news = this.db.News.Find(id);

            if (news != null)
            {
                news.Title = title;
                news.ImageUrl = imageUrl;

                await this.db.SaveChangesAsync();
            }
        }

        public async Task<bool> Delete(int id)
        {
            var news = this.db.News.Find(id);

            if (news == null)
            {
                return false;
            }

            var newsComments = this.db.Comments.Where(c => c.NewsId == id);

            foreach (var comment in newsComments)
            {
                comment.NewsId = null;
            }

            this.db.Remove(news);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<NewsTopThreeBindingModel>> TopThree()
        {
           return await this.db
                .News
                .OrderByDescending(m => m.Votes.Select(v => v.Value).Sum())
                .Take(3)
                .ProjectTo<NewsTopThreeBindingModel>()
                .ToListAsync();
        }

        public async Task<int> Votes(int newsId)
        {
            if (!await this.DoesNewsExistAsync(newsId))
            {
                return -1;
            }

            return this.db.NewsVotes.Where(v => v.NewsId == newsId).Select(v => v.Value).DefaultIfEmpty(0).Sum();
        }

        public Task<NewsWithCommentsBindingModel> WithCommentsById(int id)
        {
           return this.db
                .News
                .Where(m => m.Id == id)
                .ProjectTo<NewsWithCommentsBindingModel>()
                .FirstOrDefaultAsync();
        }

        private int NormalizeValue(int value)
        {
            if (value > 1)
            {
                value = 1;
            }
            if (value < -1)
            {
                value = -1;
            }
            return value;
        }
    }
}
