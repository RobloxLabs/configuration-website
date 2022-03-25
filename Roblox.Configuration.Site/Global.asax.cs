using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Roblox.Configuration.Site.Clients.ConfigurationService;

namespace Roblox.Configuration.Site
{
    public class MvcApplication : HttpApplication
    {
        private static ConfigurationClient _ConfigurationClient;
        public static ConfigurationClient ConfigurationClient
        {
            get { return _ConfigurationClient; }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _ConfigurationClient = new ConfigurationClient(
                () => RobloxEnvironment.GetInternalApiServiceEndpoint("configuration")
            );
        }
    }
}
