namespace StarCraftNews.Services.Comments
{
    using Microsoft.EntityFrameworkCore;
    using StarCraftNews.Data;
    using StarCraftNews.Data.Models;
    using StarCraftNews.Services.Comments.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CommentService : ICommentService
    {
        private readonly StarCraftNewsDbContext db;

        public CommentService(StarCraftNewsDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string authorId, string content, int? newsId, int? parentCommentId)
        {
            Comment comment = new Comment
            {
                AuthorId = authorId,
                Content = content,
                NewsId = newsId,
                ParentCommentId = parentCommentId,
                CreatedOn = DateTime.Now
            };

            db.Add(comment);
            await this.db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var comment = this.db.Comments.Find(id);

            if (comment != null)
            {

                var subComments = this.db.Comments.Where(c => c.ParentCommentId == id);

                foreach (var subComment in subComments)
                {
                    subComment.NewsId = null;
                }

                this.db.Remove(comment);
                await this.db.SaveChangesAsync();
            }
        }

        public async Task<int?> NewsId(int commentId)
            => await this.db.Comments
                .Where(c => (c.Id == commentId || c.ChildrenComments.Any(cc => cc.Id == commentId)) && c.NewsId != null)
                .Select(c => c.NewsId)
                .FirstOrDefaultAsync();

        public async Task<string> AuthorId(int id)
            => await this.db.Comments.Where(m => m.Id == id).Select(m => m.AuthorId).FirstOrDefaultAsync();
    }
}
