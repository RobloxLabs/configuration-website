using System;
using System.Web.Mvc;
using Roblox.Configuration.Client;
using Roblox.Configuration.Site.ViewModels.Configuration;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    [RoutePrefix("Settings")]
    //[RoutePrefix("settings")]
    public class SettingsController : Controller
    {
        private readonly IConfigurationClient _ConfigurationClient;

        public SettingsController(IConfigurationClient configurationClient)
        {
            _ConfigurationClient = configurationClient ?? throw new ArgumentNullException(nameof(configurationClient));
        }

        // GET: Settings/Search
        [HttpGet]
        //[Route("list")]
        public ActionResult Search()
        {
            var viewModel = new ConfigListViewModel
            {
                ConfigGroupNames = _ConfigurationClient.GetConfigurationGroupNames()
            };

            return View(viewModel);
        }
    }
}
