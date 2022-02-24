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
        // GET: Configuration/Search
        [HttpGet]
        //[Route("list")]
        public ActionResult Search()
        {
            var viewModel = new ConfigListViewModel();

            int count = 200; // Temp
            
            // Populate Config Groups
            var configGroups = new List<ConfigGroupModel>();
            for (int i = 1; i <= count; i++)
            {
                configGroups.Add(new ConfigGroupModel
                {
                    Id = i,
                    Name = $"Roblox.Test{i.ToString()}.Properties.Settings"
                });
            }
            viewModel.ConfigGroups = configGroups; // We can't add to an IEnumerable, so we do this instead.
            // TODO: Cast viewModel.ConfigGroups to List to add?

            return View(viewModel);
        }
    }
}
