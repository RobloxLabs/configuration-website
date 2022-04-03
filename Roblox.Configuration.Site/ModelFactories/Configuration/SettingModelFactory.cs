using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Roblox.Configuration.Site.Clients.ConfigurationService;
using Roblox.Configuration.Site.Models.Configuration;

namespace Roblox.Configuration.Site.ModelFactories.Configuration
{
    public class SettingModelFactory
    {
        public static IEnumerable<SettingModel> GetSettings(string groupName, int pageSize, int page)
        {
            IEnumerable<Setting> settings = Enumerable.Empty<Setting>();
            List<SettingModel> settingModels = new List<SettingModel>();

            if (!string.IsNullOrEmpty(groupName))
                settings = MvcApplication.ConfigurationClient.GetSettings(
                    groupName,  // Group name
                    pageSize,         // Page size
                    page           // Page number
                );

            foreach (Setting setting in settings)
            {
                settingModels.Add(new SettingModel(setting));
            }

            return settingModels;
        }
    }
}