namespace StarCraftNews.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using StarCraftNews.Data;
    using StarCraftNews.Services.Admin.Models;

    public class AdminUserService : IAdminUserService
    {
        private readonly StarCraftNewsDbContext db;

        public AdminUserService(StarCraftNewsDbContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
        {
            var users = await this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();

           return users;
        }
    }
}
