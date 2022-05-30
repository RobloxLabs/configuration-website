using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roblox.ApiControlPlane.Client;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private ApiControlPlaneClient Client { get { return MvcApplication.ApiControlPlaneClient; } }

        // GET: Services/Config
        [HttpGet]
        public ActionResult Config()
        {
            var model = Client.GetServices(0, Properties.Settings.Default.MaxServicesResults);

            return View(model);
        }
    }
}