using System;
using System.Linq;
using System.Collections.Generic;
using Roblox.Configuration.Client;
using Roblox.Configuration.Site.Models.Configuration;

namespace Roblox.Configuration.Site.ModelFactories.Configuration
{
    public class ConnectionStringModelFactory
    {
        private readonly IConfigurationClient _ConfigurationClient;

        public ConnectionStringModelFactory(IConfigurationClient configurationClient)
        {
            _ConfigurationClient = configurationClient ?? throw new ArgumentNullException(nameof(configurationClient));
        }

        public IEnumerable<ConnectionStringModel> GetConnectionStrings(string groupName, int pageSize, int page)
        {
            var connectionStrings = Enumerable.Empty<Client.IConnectionString>();
            var csModels = new List<ConnectionStringModel>();

            if (!string.IsNullOrEmpty(groupName))
                connectionStrings = _ConfigurationClient.GetConnectionStrings(
                    groupName,  // Group name
                    pageSize,   // Page size
                    page        // Page number
                );

            foreach (var cs in connectionStrings)
            {
                csModels.Add(new ConnectionStringModel(cs));
            }

            return csModels;
        }
    }
}