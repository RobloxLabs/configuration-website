using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Roblox.Configuration.Site.Models.Configuration;
using Roblox.Configuration.Site.ViewModels.Configuration;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    [RoutePrefix("Settings")]
    //[RoutePrefix("settings")]
    public class SettingsController : Controller
    {
        // GET: Settings/Search
        [HttpGet]
        //[Route("list")]
        public ActionResult Search()
        {
            var viewModel = new ConfigListViewModel
            {
                ConfigGroupNames = MvcApplication.ConfigurationClient.GetGroupNames()
            };

            return View(viewModel);
        }
    }
}
