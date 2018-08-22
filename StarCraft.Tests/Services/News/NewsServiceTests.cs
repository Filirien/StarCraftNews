﻿namespace StarCraft.Tests.Services.News
{
    using Microsoft.EntityFrameworkCore;
    using StarCraftNews.Common.News.BindingModels;
    using StarCraftNews.Data;
    using StarCraftNews.Data.Models;
    using StarCraftNews.Services.News;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class NewsServiceTests
    {
        public NewsServiceTests()
        {
            Tests.Initialize();
        }

        private readonly int NewsPageSize = 2;

        private const int NewsIdForEditing = 3;
        private const string NewsTitle = "Edited";
        private const string NewsDescription = "Description";
        private const string NewsImageUrl = "/images.jpg";

        private StarCraftNewsDbContext GetDbContext()
            => new StarCraftNewsDbContext(
                new DbContextOptionsBuilder<StarCraftNewsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        private readonly IEnumerable<News> testData = new List<News>
        {
            new News{ Id = 4, Title = "Title1",Description="Description1", ImageUrl = "/image1.jpg", CreatedOn = DateTime.Now, Author = new User{ Id = "xdas", UserName = "Ivan" }, Comments = new List<Comment>(), Votes = new List<NewsVote>()},
            new News{ Id = 2, Title = "Title2",Description="Description2", ImageUrl = "/image2.jpg", CreatedOn = DateTime.Now, Author = new User{ Id = "xdas", UserName = "Ivan" }, Comments = new List<Comment>(), Votes = new List<NewsVote>()},
            new News{ Id = 5, Title = "Title3",Description="Description3", ImageUrl = "/image3.jpg", CreatedOn = DateTime.Now, Author = new User{ Id = "xdas", UserName = "Vasil" }, Comments = new List<Comment>(), Votes = new List<NewsVote>()},
            new News{ Id = 1, Title = "Title4",Description="Description4", ImageUrl = "/image4.jpg", CreatedOn = DateTime.Now, Author = new User{ Id = "xdas", UserName = "Ivan" }, Comments = new List<Comment>(), Votes = new List<NewsVote>()},
            new News{ Id = 3, Title = "Title5",Description="Description5", ImageUrl = "/image5.jpg", CreatedOn = DateTime.Now, Author = new User{ Id = "xdas", UserName = "Ivan" }, Comments = new List<Comment>(), Votes = new List<NewsVote>()}
        };

        private void PopulateData(StarCraftNewsDbContext db)
        {
            db.AddRange(this.testData);
            db.SaveChanges();
        }

        private bool CompareNewsWithNewsListingServiceModelExact(NewsListingBindingViewModel thisNews, News otherNews)
            => thisNews.Id == otherNews.Id
            && thisNews.Title == otherNews.Title
            && thisNews.Description == otherNews.Description
            && thisNews.ImageUrl == otherNews.ImageUrl
            && thisNews.CreatedOn == otherNews.CreatedOn;

        [Fact]
        public async Task NewsServiceAllAsyncShould_ReturnsNewsForPageOneByDefault()
        {
            // Arrange

            var context = this.GetDbContext();

            this.PopulateData(context);

            var newsService = new NewsService(context);

            // Act

            var returnedData = await newsService.AllAsync();

            var expectedFirstPageNews = context.News.OrderByDescending(m => m.CreatedOn).Take(NewsPageSize).ToList();

            // Assert

            foreach (var returnedModel in returnedData)
            {
                var testModel = expectedFirstPageNews.First(n => returnedModel.Id == n.Id);

                Assert.NotNull(testModel);
                Assert.True(CompareNewsWithNewsListingServiceModelExact(returnedModel, testModel));
            }
        }
    }
}