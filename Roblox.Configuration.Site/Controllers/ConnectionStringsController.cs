using System;
using System.Web.Mvc;
using Roblox.Configuration.Client;
using Roblox.Configuration.Site.ViewModels.Configuration;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    [RoutePrefix("ConnectionStrings")]
    public class ConnectionStringsController : Controller
    {
        private readonly IConfigurationClient _ConfigurationClient;

        public ConnectionStringsController(IConfigurationClient configurationClient)
        {
            _ConfigurationClient = configurationClient ?? throw new ArgumentNullException(nameof(configurationClient));
        }

        // GET: ConnectionStrings/Search
        [HttpGet]
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
