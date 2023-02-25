using System;
using System.Linq;
using System.Collections.Generic;
using Roblox.Configuration.Client;
using Roblox.Configuration.Site.Models.Configuration;

namespace Roblox.Configuration.Site.ModelFactories.Configuration
{
    public class SettingModelFactory
    {
        private readonly IConfigurationClient _ConfigurationClient;

        public SettingModelFactory(IConfigurationClient configurationClient)
        {
            _ConfigurationClient = configurationClient ?? throw new ArgumentNullException(nameof(configurationClient));
        }

        
        public IEnumerable<SettingModel> GetSettings(string groupName, int pageSize, int page)
        {
            var settings = Enumerable.Empty<Client.ISetting>();
            var settingModels = new List<SettingModel>();

            if (!string.IsNullOrEmpty(groupName))
                settings = _ConfigurationClient.GetSettings(
                    groupName,  // Group name
                    pageSize,         // Page size
                    page           // Page number
                );

            foreach (var setting in settings)
            {
                settingModels.Add(new SettingModel(setting));
            }

            return settingModels;
        }
    }
}