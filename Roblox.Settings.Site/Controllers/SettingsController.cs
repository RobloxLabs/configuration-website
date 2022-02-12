using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Roblox.Settings.Site.Models.Configuration;
using Roblox.Settings.Site.ViewModels.Configuration;

namespace Roblox.Settings.Site.Controllers
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
