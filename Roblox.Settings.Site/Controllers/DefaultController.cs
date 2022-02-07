using System.Web.Mvc;
using Roblox.Settings.Site.ViewModels;

namespace Roblox.Settings.Site.Controllers
{
    [RoutePrefix("")]
    public class DefaultController : Controller
    {
        private RedirectToRouteResult RedirectToLogin() => RedirectToAction("Login", "Account", new { ReturnUrl = Request.Url.PathAndQuery });

        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated) return RedirectToLogin();

            ViewBag.UserName = HttpContext.User.Identity.Name;

            return View();
        }

        // Max per page is 25
        [HttpGet]
        [Route("list-groups")]
        public ActionResult ListConfigurationGroups(int page = 0)
        {
            if (!HttpContext.User.Identity.IsAuthenticated) return RedirectToLogin();

            ViewBag.UserName = HttpContext.User.Identity.Name;

            return View(new PaginationViewModel<string> { Items = new[] { "Test", "Test2" }, CountRemaining = 0 });
        }
    }
}
