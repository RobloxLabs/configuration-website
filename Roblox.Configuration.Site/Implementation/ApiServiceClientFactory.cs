using System;
using Roblox.Configuration.Client;
using Roblox.ApiControlPlane.Client;

namespace Roblox.Configuration.Site.Implementation
{
    internal static class ApiServiceClientFactory
    {
        private delegate T ApiClientGetter<T>(Func<string> endpointGetter, Func<string> apiKeyGetter);

        private static T GetApiServiceClient<T>(ApiClientGetter<T> clientGetter, string serviceName, Func<string> serviceUrlGetter = null, Func<string> apiKeyGetter = null)
            where T : ApiClientBase.ApiClientBase
        {
            if (clientGetter == null) throw new ArgumentNullException(nameof(clientGetter));
            if (string.IsNullOrEmpty(serviceName)) throw new ArgumentNullException(nameof(serviceName));

            Func<string> endpointGetter;
            
            if (serviceUrlGetter != null)
            {
                endpointGetter = () =>
                {
                    string endpoint = serviceUrlGetter();
                    if (string.IsNullOrEmpty(endpoint))
                        endpoint = RobloxEnvironment.GetInternalApiServiceEndpoint(serviceName);
                    return endpoint;
                };
            }
            else
            {
                // No serviceUrlGetter specified
                endpointGetter = () => RobloxEnvironment.GetInternalApiServiceEndpoint(serviceName);
            }

            var client = clientGetter(
                endpointGetter,
                apiKeyGetter
            );

            return client;
        }

        public static IConfigurationClient GetConfigurationClient()
        {
            return GetApiServiceClient(
                serviceName: "configuration",
                serviceUrlGetter: () => global::Roblox.Configuration.Site.Properties.Settings.Default.ConfigurationServiceUrl,
                apiKeyGetter: () => global::Roblox.Configuration.Site.Properties.Settings.Default.ConfigurationServiceApiKey,
                clientGetter: (url, key) => new ConfigurationClient(url, key)
            );
        }

        public static IApiControlPlaneClient GetApiControlPlaneClient()
        {
            return GetApiServiceClient(
                serviceName: "apicontrolplane",
                clientGetter: (url, key) => new ApiControlPlaneClient(url, key)
            );
        }
    }
}
