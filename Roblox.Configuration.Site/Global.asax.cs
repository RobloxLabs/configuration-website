using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Unity;
using Unity.Mvc5;
using Roblox.EventLog;
using Roblox.Configuration.Client;
using Roblox.Configuration.Site.ModelFactories.Configuration;
using Roblox.Configuration.Site.Clients.ConfigurationService;
using Roblox.ApiControlPlane.Client;

namespace Roblox.Configuration.Site
{
    public class MvcApplication : HttpApplication
    {
        private static ILogger Logger { get; set; }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();

            if (ex is not HttpException)
                Logger?.Error(ex);
        }

        private static ApiControlPlaneClient _ApiControlPlaneClient;
        public static ApiControlPlaneClient ApiControlPlaneClient
        {
            get { return _ApiControlPlaneClient; }
        }

        protected void Application_Start()
        {
            var container = new UnityContainer();

            Logger = new Logger(
                global::Roblox.Configuration.Site.Properties.Settings.Default.LogName,
                () => global::Roblox.Configuration.Site.Properties.Settings.Default.LogLevel,
                false
            )
            {
                LogThreadID = true,
                LogToConsole = false
            };

            var serviceUri = global::Roblox.Configuration.Site.Properties.Settings.Default.ConfigurationServiceUrl;
            var serviceApiKey = global::Roblox.Configuration.Site.Properties.Settings.Default.ConfigurationServiceApiKey;

            var configurationClient = new ConfigurationClient(
                () => !string.IsNullOrEmpty(serviceUri) ? serviceUri : RobloxEnvironment.GetInternalApiServiceEndpoint("configuration"),
                !string.IsNullOrEmpty(serviceApiKey) ? () => serviceApiKey : null
            );

            container.RegisterInstance<ILogger>(Logger);
            container.RegisterInstance<IConfigurationClient>(configurationClient);
            container.RegisterType<ConnectionStringModelFactory>();
            container.RegisterType<SettingModelFactory>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
