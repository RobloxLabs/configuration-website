﻿using System;
using System.Web;
using System.Web.Mvc;
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
                ConfigGroupNames = MvcApplication.ConfigurationClient.GetGroupNames(50, 0)
            };

            return View(viewModel);
        }
    }
}
