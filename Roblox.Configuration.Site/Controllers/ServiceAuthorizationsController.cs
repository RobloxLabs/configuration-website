using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roblox.ApiControlPlane.Client;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    public class ServiceAuthorizationsController : Controller
    {
        private ApiControlPlaneClient Client { get { return MvcApplication.ApiControlPlaneClient; } }

        // GET: ServiceAuthorizations/Config
        [HttpGet]
        public ActionResult Config(int clientId)
        {
            var client = Client.GetApiClient(clientId);
            ViewBag.ClientKey = client.Key.ToString();
            ViewBag.CurrentClientName = client.Note;
            var model = Client.GetAllClientAuthorizations(client.Key, 0, Properties.Settings.Default.MaxClientAuthorizationsResults);

            return View(model);
        }
    }
}