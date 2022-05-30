using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Roblox.ApiControlPlane.Client;

namespace Roblox.Configuration.Site.Controllers
{
    [Authorize]
    [RoutePrefix("ServicesConfig")]
    public class ServicesConfigController : Controller
    {
        private ApiControlPlaneClient Client { get { return MvcApplication.ApiControlPlaneClient; } }


        // GET: ServicesConfig/GenerateGuid
        [HttpGet]
        public ActionResult GenerateGuid()
        {
            return Content(Guid.NewGuid().ToString());
        }

        // POST: ServicesConfig/AddClient
        [HttpPost]
        public void AddClient(string key, string note)
        {
            Guid guidKey = Guid.Empty;
            if (!string.IsNullOrEmpty(key) && !Guid.TryParse(key, out guidKey))
                throw new ArgumentException("Key isn't a valid GUID");

            Client.AddClient(guidKey != Guid.Empty ? guidKey : (Guid?)null, note);
        }

        // POST: ServicesConfig/RemoveClient
        [HttpPost]
        public void RemoveClient(int id)
        {
            Client.RemoveClient(id);
        }

        // POST: ServicesConfig/DuplicateClient
        [HttpPost]
        public void DuplicateClient(int id, string key, string note)
        {
            Guid guidKey;
            if (!Guid.TryParse(key, out guidKey))
                throw new ArgumentException("Key isn't a valid GUID");

            Client.DuplicateClient(id, guidKey, note);
        }

        // GET: ServicesConfig/RevealClientKey
        [HttpGet]
        public ActionResult RevealClientKey(int clientId)
        {
            var apiClient = Client.GetApiClient(clientId);

            return Content(apiClient.Key.ToString());
        }

        // GET: ServicesConfig/SearchApiClientIDListByKey
        [HttpGet]
        public ActionResult SearchApiClientIDListByKey(string clientKey)
        {
            Guid key;
            if (Guid.TryParse(clientKey, out key))
            {
                var client = Client.GetApiClientByKey(key);
                if (client != null)
                    return Json(new int[] { client.ID }, JsonRequestBehavior.AllowGet);
            }

            return new EmptyResult();
        }

        // POST: ServicesConfig/UpdateClient
        [HttpPost]
        public void UpdateClient(bool isValid, int id, string note, Guid key)
        {
            Client.UpdateClient(id, note, key, isValid);
        }

        // POST: ServicesConfig/ToggleClient
        [HttpPost]
        public void ToggleClient(bool isValid, int id)
        {
            Client.SetClient(id, isValid);
        }

        // POST: ServicesConfig/AddService
        [HttpPost]
        public void AddService(string name)
        {
            Client.AddService(name);
        }

        // POST: ServicesConfig/UpdateService
        [HttpPost]
        public void UpdateService(int serviceId, string serviceName, bool enableService)
        {
            Client.UpdateService(serviceId, serviceName, enableService);
        }

        // POST: ServicesConfig/ToggleService
        [HttpPost]
        public void ToggleService(int serviceId, bool enableService)
        {
            Client.SetService(serviceId, enableService);
        }

        // POST: ServicesConfig/AddOperation
        [HttpPost]
        public void AddOperation(string operationName, string serviceName)
        {
            Client.AddOperation(operationName, serviceName);
        }

        // POST: ServicesConfig/UpdateOperation
        [HttpPost]
        public void UpdateOperation(int operationId, string operationName, string serviceName, bool enableOperation)
        {
            Client.UpdateOperation(operationId, operationName, serviceName, enableOperation);
        }

        // POST: ServicesConfig/DeleteOperation
        [HttpPost]
        public void DeleteOperation(int operationId)
        {
            Client.DeleteOperation(operationId);
        }

        // POST: ServicesConfig/AddServiceAuthorization
        [HttpPost]
        public void AddServiceAuthorization(Guid key, string serviceName, string authorizationType)
        {
            Client.AddServiceAuthorization(key, serviceName, authorizationType);
        }

        // POST: ServicesConfig/AddOperationAuthorization
        [HttpPost]
        public void AddOperationAuthorization(Guid key, string serviceName, string operationName, string authorizationType)
        {
            Client.AddOperationAuthorization(key, serviceName, operationName, authorizationType);
        }

        // GET: ServicesConfig/GetOperations
        [HttpGet]
        public ActionResult GetOperations(string serviceName)
        {
            var operations = Client.GetOperationsByService(serviceName, 0, 200);

            return Json(operations);
        }
    }
}
