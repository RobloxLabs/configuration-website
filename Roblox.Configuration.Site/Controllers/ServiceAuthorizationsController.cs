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
    public class ServiceAuthorizationsController : Controller
    {
        private readonly ILogger _Logger;
        private readonly IApiControlPlaneClient _ApiControlPlaneClient;

        public ServiceAuthorizationsController(ILogger logger, IApiControlPlaneClient apiControlPlaneClient)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ApiControlPlaneClient = apiControlPlaneClient ?? throw new ArgumentNullException(nameof(apiControlPlaneClient));
        }

        // GET: ServiceAuthorizations/Config
        [HttpGet]
        public ActionResult Config(int clientId)
        {
            var client = _ApiControlPlaneClient.GetApiClient(clientId);
            ViewBag.ClientKey = client.Key.ToString();
            ViewBag.CurrentClientName = client.Note;
            var model = _ApiControlPlaneClient.GetAllClientAuthorizations(client.Key, 0, Properties.Settings.Default.MaxClientAuthorizationsResults);

            return View(model);
        }
    }
}