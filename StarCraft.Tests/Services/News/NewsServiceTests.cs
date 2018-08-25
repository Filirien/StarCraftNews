namespace StarCraft.Tests.Services.News
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using StarCraftNews.Common.News.BindingModels;
    using StarCraftNews.Data;
    using StarCraftNews.Data.Models;
    using StarCraftNews.Services.News;
    using StarCraftNews.Web.Infrastructure.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class NewsServiceTests
    {
        public NewsServiceTests()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            mapper = new Mapper(configuration);
        }

        private readonly int NewsPageSize = 6;

        private const int NewsIdForDeleting = 1;

        private const int NewsIdForEditing = 3;
        private const string NewsTitle = "Edited";
        private const string NewsDescription = "Description";
        private const string NewsImageUrl = "/images.jpg";
        private IMapper mapper;

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
            //Arrange

            var context = this.GetDbContext();

            this.PopulateData(context);

            var newsService = new NewsService(context, mapper);

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

        [Fact]
        public async Task NewsServiceEditShould_EditEntity()
        {
            // Arrange

            var context = this.GetDbContext();

            this.PopulateData(context);

            var newsService = new NewsService(context, mapper);

            // Act

            await newsService.Edit(NewsIdForEditing, NewsTitle, NewsDescription, NewsImageUrl);

            // Assert
            var actualNews = context.News.Find(NewsIdForEditing);

            Assert.NotNull(actualNews);
            Assert.Equal(NewsTitle, actualNews.Title);
            Assert.Equal(NewsDescription, actualNews.Description);
            Assert.Equal(NewsImageUrl, actualNews.ImageUrl);
        }

        [Fact]
        public async Task NewsServiceDeleteShould_DeleteEntry()
        {
            // Arrange

            var context = this.GetDbContext();

            this.PopulateData(context);

            var newsService = new NewsService(context, mapper);

            // Act

            await newsService.Delete(NewsIdForDeleting);

            // Assert
            Assert.True(!context.News.Any(m => m.Id == NewsIdForDeleting));

        }
    }
}
