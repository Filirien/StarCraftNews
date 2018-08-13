namespace StarCraftNews.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static StarCraftNews.Web.WebConstants;

    [Area(AdminArea)]
    [Authorize(Roles = AdministratorRole)]
    public abstract class BaseAdminController : Controller
    {
    }
}