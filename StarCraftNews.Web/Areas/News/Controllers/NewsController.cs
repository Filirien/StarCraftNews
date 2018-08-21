namespace StarCraftNews.Web.Areas.News.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StarCraftNews.Common.News.BindingModels;
    using StarCraftNews.Common.News.ViewModels;
    using StarCraftNews.Data.Models;
    using StarCraftNews.Services.News.Interfaces;
    using StarCraftNews.Web.Infrastructure.Extensions;
    using StarCraftNews.Web.Infrastructure.Filters;

    [Authorize]
    public class NewsController : Controller
    {
        private readonly INewsService news;
        private readonly UserManager<User> userManager;

        public NewsController(INewsService news, UserManager<User> userManager)
        {
            this.news = news;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]int page = 1)
            => View(await this.news.AllAsync(page));

        [AllowAnonymous]
        public async Task<IActionResult> AllAsync([FromQuery]int page = 1)
            => Json(await this.news.AllAsync(page));

        public IActionResult Create()
            => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(NewsCreateViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            await this.news.CreateAsync(userId, model.Title, model.ImageUrl);

            TempData.AddSuccessMessage($"News created successfully!");

            return RedirectToAction(nameof(All));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details([FromRoute]int id)
        {
            var news = await this.news.WithCommentsById(id);
            return this.ViewOrNotFound(news);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddOrUpdateVote(NewsVoteViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            return this.Ok(await this.news.AddOrUpdateVote(model.NewsId, userId, model.Value));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var newsAuthorId = await this.news.AuthorId(id);
            if (newsAuthorId != userId && !User.IsInRole("Administrator"))
            {
                return BadRequest();
            }
            return View(await this.news.ById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewsMinifieldBindingModel model)
        {
            var userId = this.userManager.GetUserId(User);
            var newsAuthorId = await this.news.AuthorId(id);
            if (newsAuthorId != userId && !User.IsInRole("Administrator"))
            {
                return BadRequest();
            }

            await this.news.Edit(id, model.Title, model.ImageUrl);

            return RedirectToAction(nameof(All));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var newsAuthorId = await this.news.AuthorId(id);
            if (newsAuthorId != userId && !User.IsInRole("Administrator"))
            {
                return BadRequest();
            }
            return View(await this.news.ById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, NewsMinifieldBindingModel model)
        {
            var userId = this.userManager.GetUserId(User);
            var newsAuthorId = await this.news.AuthorId(id);
            if (newsAuthorId != userId && !User.IsInRole("Administrator"))
            {
                return BadRequest();
            }

            var success = await this.news.Delete(id);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
    }
}