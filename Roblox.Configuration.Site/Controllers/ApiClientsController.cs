using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roblox.ApiControlPlane.Client;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    public class ApiClientsController : Controller
    {
        private ApiControlPlaneClient Client { get { return MvcApplication.ApiControlPlaneClient; } }

        // GET: ApiClients/Search
        [HttpGet]
        public ActionResult Search()
        {
            var model = Client.GetApiClients(0, Properties.Settings.Default.MaxApiClientResults);

            return View(model);
        }
    }
}