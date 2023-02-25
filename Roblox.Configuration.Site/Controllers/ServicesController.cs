using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roblox.EventLog;
using Roblox.ApiControlPlane.Client;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly ILogger _Logger;
        private readonly IApiControlPlaneClient _ApiControlPlaneClient;

        public ServicesController(ILogger logger, IApiControlPlaneClient apiControlPlaneClient)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ApiControlPlaneClient = apiControlPlaneClient ?? throw new ArgumentNullException(nameof(apiControlPlaneClient));
        }

        // GET: Services/Config
        [HttpGet]
        public ActionResult Config()
        {
            var model = _ApiControlPlaneClient.GetServices(0, Properties.Settings.Default.MaxServicesResults);

            return View(model);
        }
    }
}