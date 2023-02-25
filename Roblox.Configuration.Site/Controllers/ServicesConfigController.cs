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
    [RoutePrefix("ServicesConfig")]
    public class ServicesConfigController : Controller
    {
        private readonly ILogger _Logger;
        private readonly IApiControlPlaneClient _ApiControlPlaneClient;

        public ServicesConfigController(ILogger logger, IApiControlPlaneClient apiControlPlaneClient)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ApiControlPlaneClient = apiControlPlaneClient ?? throw new ArgumentNullException(nameof(apiControlPlaneClient));
        }


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

            _ApiControlPlaneClient.AddClient(
                key: guidKey != Guid.Empty ? guidKey : (Guid?)null,
                note: note
            );
        }

        // POST: ServicesConfig/RemoveClient
        [HttpPost]
        public void RemoveClient(int id)
        {
            _ApiControlPlaneClient.RemoveClient(id: id);
        }

        // POST: ServicesConfig/DuplicateClient
        [HttpPost]
        public void DuplicateClient(int id, string key, string note)
        {
            Guid guidKey;
            if (!Guid.TryParse(key, out guidKey))
                throw new ArgumentException("Key isn't a valid GUID");

            _ApiControlPlaneClient.DuplicateClient(id: id, key: guidKey, note: note);
        }

        // GET: ServicesConfig/RevealClientKey
        [HttpGet]
        public ActionResult RevealClientKey(int clientId)
        {
            var apiClient = _ApiControlPlaneClient.GetApiClient(id: clientId);

            return Content(apiClient.Key.ToString());
        }

        // GET: ServicesConfig/SearchApiClientIDListByKey
        [HttpGet]
        public ActionResult SearchApiClientIDListByKey(string clientKey)
        {
            Guid key;
            if (Guid.TryParse(clientKey, out key))
            {
                var client = _ApiControlPlaneClient.GetApiClientByKey(key: key);
                if (client != null)
                    return Json(new int[] { client.ID }, JsonRequestBehavior.AllowGet);
            }

            return new EmptyResult();
        }

        // POST: ServicesConfig/UpdateClient
        [HttpPost]
        public void UpdateClient(bool isValid, int id, string note, Guid key)
        {
            _ApiControlPlaneClient.UpdateClient(
                id: id,
                isValid: isValid,
                note: note,
                key: key
            );
        }

        // POST: ServicesConfig/ToggleClient
        [HttpPost]
        public void ToggleClient(bool isValid, int id)
        {
            _ApiControlPlaneClient.UpdateClient(id: id, isValid: isValid);
        }

        // POST: ServicesConfig/AddService
        [HttpPost]
        public void AddService(string name)
        {
            _ApiControlPlaneClient.AddService(name: name);
        }

        // POST: ServicesConfig/UpdateService
        [HttpPost]
        public void UpdateService(int serviceId, string serviceName, bool enableService)
        {
            _ApiControlPlaneClient.UpdateService(serviceId: serviceId, serviceName: serviceName, enableService: enableService);
        }

        // POST: ServicesConfig/ToggleService
        [HttpPost]
        public void ToggleService(int serviceId, bool enableService)
        {
            _ApiControlPlaneClient.UpdateService(serviceId: serviceId, enableService: enableService);
        }

        // POST: ServicesConfig/AddOperation
        [HttpPost]
        public void AddOperation(string operationName, string serviceName)
        {
            _ApiControlPlaneClient.AddOperation(operationName: operationName, serviceName: serviceName);
        }

        // POST: ServicesConfig/UpdateOperation
        [HttpPost]
        public void UpdateOperation(int operationId, string operationName, string serviceName, bool enableOperation)
        {
            _ApiControlPlaneClient.UpdateOperation(operationId: operationId, operationName: operationName, serviceName: serviceName, enableOperation: enableOperation);
        }

        // POST: ServicesConfig/ToggleOperation
        [HttpPost]
        public void ToggleOperation(int operationId, string operationName, string serviceName, bool enableOperation)
        {
            _ApiControlPlaneClient.UpdateOperation(operationId: operationId, enableOperation: enableOperation);
        }

        // POST: ServicesConfig/DeleteOperation
        [HttpPost]
        public void DeleteOperation(int operationId)
        {
            _ApiControlPlaneClient.DeleteOperation(operationId: operationId);
        }

        // POST: ServicesConfig/AddServiceAuthorization
        [HttpPost]
        public void AddServiceAuthorization(Guid key, string serviceName, string authorizationType)
        {
            _ApiControlPlaneClient.AddServiceAuthorization(key: key, serviceName: serviceName, authorizationType: authorizationType);
        }

        // POST: ServicesConfig/AddOperationAuthorization
        [HttpPost]
        public void AddOperationAuthorization(Guid key, string serviceName, string operationName, string authorizationType)
        {
            _ApiControlPlaneClient.AddOperationAuthorization(key: key, serviceName: serviceName, operationName: operationName, authorizationType: authorizationType);
        }

        // GET: ServicesConfig/GetOperations
        [HttpGet]
        public ActionResult GetOperations(string serviceName)
        {
            var operations = _ApiControlPlaneClient.GetOperationsByService(serviceName: serviceName, startRowIndex: 0, maxRows: 200);

            return Json(operations);
        }
    }
}
