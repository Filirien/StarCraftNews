using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StarCraftNews.Data.Models;

namespace StarCraftNews.Data
{
    public class StarCraftNewsDbContext : IdentityDbContext<User>
    {
        public StarCraftNewsDbContext(DbContextOptions<StarCraftNewsDbContext> options)
            : base(options)
        {
        }

        public DbSet<News> News { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<NewsVote> NewsVotes { get; set; }

        public DbSet<CommentVote> CommentVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<News>()
                .HasOne(n => n.Author)
                .WithMany(a => a.News)
                .HasForeignKey(n => n.AuthorId);

            builder.Entity<Comment>()
                .HasOne(c => c.News)
                .WithMany(n => n.Comments)
                .HasForeignKey(c => c.NewsId);

            builder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId);

            builder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(pc => pc.ChildrenComments)
                .HasForeignKey(pc => pc.ParentCommentId);

            builder.Entity<NewsVote>()
                .HasOne(nv => nv.News)
                .WithMany(n => n.Votes)
                .HasForeignKey(nv => nv.NewsId);

            builder.Entity<NewsVote>()
                .HasOne(nv => nv.User)
                .WithMany(u => u.NewsVotes)
                .HasForeignKey(nv => nv.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CommentVote>()
                .HasOne(cv => cv.Comment)
                .WithMany(c => c.Votes)
                .HasForeignKey(cv => cv.CommentId);

            builder.Entity<CommentVote>()
                .HasOne(cv => cv.User)
                .WithMany(u => u.CommentVotes)
                .HasForeignKey(cv => cv.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
