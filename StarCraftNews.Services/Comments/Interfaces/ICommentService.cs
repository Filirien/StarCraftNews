namespace StarCraftNews.Services.Comments.Interfaces
{
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task CreateAsync(string authorId, string content, int? newsId, int? parentCommentId);

        Task<int?> NewsId(int commentId);

        Task Delete(int id);

        Task<string> AuthorId(int id);
    }
}
