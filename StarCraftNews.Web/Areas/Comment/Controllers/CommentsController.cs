namespace StarCraftNews.Web.Areas.Comment.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using StarCraftNews.Common.Comments.ViewModels;
    using StarCraftNews.Data.Models;
    using StarCraftNews.Services.Comments.Interfaces;
    using StarCraftNews.Web.Infrastructure.Extensions;
    using StarCraftNews.Web.Infrastructure.Filters;

    using static StarCraftNews.Web.WebConstants;

    [Area("Comment")]
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ICommentService comments;
        private readonly UserManager<User> userManager;

        public CommentsController(ICommentService comments, UserManager<User> userManager)
        {
            this.comments = comments;
            this.userManager = userManager;
        }

        public IActionResult Create([FromQuery]int? newsId, [FromQuery]int? parentCommentId)
        {
            if ((newsId == null && parentCommentId == null) || (newsId != null && parentCommentId != null))
            {
                return BadRequest();
            }

            var model = new CommentCreateViewModel
            {
                NewsId = newsId,
                ParentCommentId = parentCommentId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(CommentCreateViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            await this.comments.CreateAsync(userId, model.Content, model.NewsId, model.ParentCommentId);

            if (model.NewsId == null)
            {
                model.NewsId = await this.comments.NewsId(model.ParentCommentId.Value);
            }

            TempData.AddSuccessMessage($"Comment created successfully!");

            return RedirectToAction("Details", "News", new { id = model.NewsId, area = "News" });
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var newsAuthorId = await this.comments.AuthorId(id);
            if (newsAuthorId != userId && !User.IsInRole(AdministratorRole))
            {
                return BadRequest();
            }

            var newsId = await this.comments.NewsId(id);

            if (newsId == null)
            {
                return BadRequest();
            }

            await this.comments.Delete(id);

            return RedirectToAction("Details", "News", new { id = newsId, area = "News" });
        }
    }
}