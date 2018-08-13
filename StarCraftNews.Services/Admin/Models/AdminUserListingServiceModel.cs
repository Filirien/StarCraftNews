namespace StarCraftNews.Services.Admin.Models
{
    using StarCraftNews.Common.Mapping;
    using StarCraftNews.Data.Models;

    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
