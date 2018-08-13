namespace StarCraftNews.Web.Areas.Admin.Models.Users
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using StarCraftNews.Services.Admin.Models;
    using System.Collections.Generic;

    public class AdminUserListingsViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
