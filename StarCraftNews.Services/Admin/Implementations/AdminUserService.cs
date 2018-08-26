namespace StarCraftNews.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;
    using StarCraftNews.Data;
    using StarCraftNews.Services.Admin.Models;

    public class AdminUserService : IAdminUserService
    {
        private readonly StarCraftNewsDbContext db;
        private readonly IMapper mapper;

        public AdminUserService(StarCraftNewsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
        {
            var users = await this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>(mapper.ConfigurationProvider)
                .ToListAsync();

           return users;
        }
    }
}
