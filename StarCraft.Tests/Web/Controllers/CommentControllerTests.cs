namespace StarCraftNews.Tests.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using StarCraft.Tests.Mocks;
    using StarCraftNews.Data;
    using StarCraftNews.Data.Models;
    using StarCraftNews.Web.Areas.Comment.Controllers;
    using StarCraftNews.Web.Areas.News.Controllers;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using Xunit;

    public class CommentControllerTests
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
        public void NewsControllerShould_BeOnlyForRegisteredUsers()
        {
            // Arrange
            var controller = typeof(CommentsController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            Assert.NotNull(areaAttribute);
        }

    }
}
