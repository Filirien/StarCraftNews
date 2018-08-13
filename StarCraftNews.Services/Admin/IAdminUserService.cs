namespace StarCraftNews.Services.Admin
{
    using StarCraftNews.Services.Admin.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync();
    }
}
