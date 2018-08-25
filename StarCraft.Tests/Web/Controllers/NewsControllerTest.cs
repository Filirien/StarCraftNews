namespace StarCraftNews.Tests.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using StarCraft.Tests.Mocks;
    using StarCraftNews.Common.News.ViewModels;
    using StarCraftNews.Data;
    using StarCraftNews.Data.Models;
    using StarCraftNews.Services.News.Interfaces;
    using StarCraftNews.Web.Areas.News.Controllers;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Xunit;

    using static StarCraftNews.Web.WebConstants;

    public class NewsControllerTest
    {
        private StarCraftNewsDbContext GetDbContext()
              => new StarCraftNewsDbContext(
                  new DbContextOptionsBuilder<StarCraftNewsDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options);

        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns("1");

            return userManager;
        }

        [Fact]
        public void MemesControllerShould_BeOnlyForRegisteredUsers()
        {
            // Arrange
            var controller = typeof(NewsController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            Assert.NotNull(areaAttribute);
        }

        //[Fact]
        //public async Task PostCreateShouldReturnRedirectWithValidModel()
        //{
        //    // Arrange
        //    string titleValue = "TestMeme";
        //    string imageUrlValue = "test.com/dadsefaef";

        //    string modelUserId = null;
        //    string modelTitle = null;
        //    string modelImageUrl = null;
        //    string successMessage = null;

        //    var userManager = this.GetUserManagerMock();

        //    var newsService = new Mock<INewsService>();
        //    newsService
        //        .Setup(c => c.CreateAsync(
        //            It.IsAny<string>(),
        //            It.IsAny<string>(),
        //            It.IsAny<string>(),
        //            It.IsAny<string>()))
        //        .Callback((string userId, string title, string imageUrl) =>
        //        {
        //            modelUserId = userId;
        //            modelTitle = title;
        //            modelImageUrl = imageUrl;
        //        })
        //        .Returns(Task.CompletedTask);

        //    var tempData = new Mock<ITempDataDictionary>();

        //    tempData
        //        .SetupSet(t => t[TempDataSuccessMessageKey] = It.IsAny<string>())
        //        .Callback((string key, object message) => successMessage = message as string);

        //    var controller = new NewsController(newsService.Object, userManager.Object);
        //    controller.TempData = tempData.Object;

        //    // Act
        //    var result = await controller.Create(new NewsCreateViewModel
        //    {
        //        Title = titleValue,
        //        ImageUrl = imageUrlValue
        //    });

        //    // Assert

        //    Assert.Equal(titleValue, modelTitle);
        //    Assert.Equal(imageUrlValue, modelImageUrl);

        //    Assert.Equal("Meme created successfully!", successMessage);

        //    Assert.IsType<RedirectToActionResult>(result);

        //    Assert.Equal("All", (result as RedirectToActionResult).ActionName);
        //    Assert.Equal(null, (result as RedirectToActionResult).ControllerName);
        //}

        [Fact]
        public void MemesControllerAllShould_BeForRegisteredUsersAndGuests()
        {
            // Arrange
            var controller = typeof(NewsControllerTest);
            var method = controller.GetMethods().Where(m => m.Name == "All").FirstOrDefault();

            // Act
            Assert.NotNull(method);
            var allowGuestsAttribute = method
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AllowAnonymousAttribute))
                as AllowAnonymousAttribute;

            // Assert
            Assert.NotNull(allowGuestsAttribute);
        }
    }
}
