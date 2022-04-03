using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Roblox.Configuration.Site.Clients.ConfigurationService;
using Roblox.Configuration.Site.Models.Configuration;

namespace Roblox.Configuration.Site.ModelFactories.Configuration
{
    public class ConnectionStringModelFactory
    {
        public static IEnumerable<ConnectionStringModel> GetConnectionStrings(string groupName, int pageSize, int page)
        {
            IEnumerable<ConnectionString> connectionStrings = Enumerable.Empty<ConnectionString>();
            List<ConnectionStringModel> csModels = new List<ConnectionStringModel>();

            if (!string.IsNullOrEmpty(groupName))
                connectionStrings = MvcApplication.ConfigurationClient.GetConnectionStrings(
                    groupName,  // Group name
                    pageSize,   // Page size
                    page        // Page number
                );

            foreach (ConnectionString cs in connectionStrings)
            {
                csModels.Add(new ConnectionStringModel(cs));
            }

            return csModels;
        }
    }
}