using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roblox.ApiControlPlane.Client;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    public class OperationsController : Controller
    {
        private ApiControlPlaneClient Client { get { return MvcApplication.ApiControlPlaneClient; } }

        // GET: Operations/Config
        [HttpGet]
        public ActionResult Config(string serviceName)
        {
            if (!string.IsNullOrEmpty(serviceName))
            {
                ViewBag.CurrentServiceName = serviceName;
                var model = Client.GetOperationsByService(serviceName, 0, Properties.Settings.Default.MaxOperationsResults);

                return View(model);
            }
            return HttpNotFound("Invalid service name");
        }
    }
}