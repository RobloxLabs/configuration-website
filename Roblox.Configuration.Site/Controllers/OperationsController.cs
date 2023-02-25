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
    public class OperationsController : Controller
    {
        private readonly ILogger _Logger;
        private readonly IApiControlPlaneClient _ApiControlPlaneClient;

        public OperationsController(ILogger logger, IApiControlPlaneClient apiControlPlaneClient)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ApiControlPlaneClient = apiControlPlaneClient ?? throw new ArgumentNullException(nameof(apiControlPlaneClient));
        }

        // GET: Operations/Config
        [HttpGet]
        public ActionResult Config(string serviceName)
        {
            if (!string.IsNullOrEmpty(serviceName))
            {
                ViewBag.CurrentServiceName = serviceName;
                var model = _ApiControlPlaneClient.GetOperationsByService(serviceName, 0, Properties.Settings.Default.MaxOperationsResults);

                return View(model);
            }
            return HttpNotFound("Invalid service name");
        }
    }
}