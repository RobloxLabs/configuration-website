using System;
using Roblox.Configuration.Site.Clients.ConfigurationService;

namespace Roblox.Configuration.Site.Models.Configuration
{
    public class SettingModel : Setting
    {
        public string LastModified { get { return Updated.ToString(); } }

        public bool IsShutdownSetting { get; set; }

        public bool IsRestrictedSetting { get; set; }

        public SettingModel()
        {
            Updated = DateTime.MinValue;
            Comment = "";
            IsEnvironmentSpecific = false;
            IsMasked = false;
            IsValueSameForAllTestEnvironments = false;
            IsValueUniqueForProduction = false;
        }

        public SettingModel(Setting s)
        {
            Id = s.Id;
            GroupName = s.GroupName;
            Name = s.Name;
            Type = s.Type;
            Value = s.Value;
            Comment = s.Comment;
            IsEnvironmentSpecific = s.IsEnvironmentSpecific;
            Updated = s.Updated;
            IsMasked = s.IsMasked;
            IsValueSameForAllTestEnvironments = s.IsValueSameForAllTestEnvironments;
            IsValueUniqueForProduction = s.IsValueUniqueForProduction;
	    }
    }
}