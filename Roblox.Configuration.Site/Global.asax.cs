using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Unity;
using Unity.Mvc5;
using Roblox.EventLog;
using Roblox.Configuration.Client;
using Roblox.Configuration.Site.ModelFactories.Configuration;
using Roblox.ApiControlPlane.Client;
using Roblox.Configuration.Site.Implementation;

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

            container.RegisterInstance<ILogger>(Logger);
            container.RegisterInstance<IConfigurationClient>(ApiServiceClientFactory.GetConfigurationClient());
            container.RegisterInstance<IApiControlPlaneClient>(ApiServiceClientFactory.GetApiControlPlaneClient());
            container.RegisterType<ConnectionStringModelFactory>();
            container.RegisterType<SettingModelFactory>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
