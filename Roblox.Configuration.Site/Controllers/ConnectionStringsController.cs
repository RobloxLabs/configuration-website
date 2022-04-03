using System;
using System.Web;
using System.Web.Mvc;
using Roblox.Configuration.Site.ViewModels.Configuration;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    [RoutePrefix("ConnectionStrings")]
    //[RoutePrefix("connection-strings")]
    public class ConnectionStringsController : Controller
    {
        // GET: ConnectionStrings/Search
        [HttpGet]
        //[Route("list")]
        public ActionResult Search()
        {
            var viewModel = new ConfigListViewModel
            {
                ConfigGroupNames = MvcApplication.ConfigurationClient.GetGroupNames(50, 0)
            };

            return View(viewModel);
        }
    }
}
