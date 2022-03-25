using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Roblox.Configuration.Site.Clients.ConfigurationService;

namespace Roblox.Configuration.Site.Models.Configuration
{
    public class SettingModel
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public string Comment { get; set; }

        public bool IsEnvironmentSpecific { get; set; }

        public string LastModified { get; set; }

        public bool IsMasked { get; set; }

        public bool IsShutdownSetting { get; set; }

        public bool IsRestrictedSetting { get; set; }

        public static implicit operator SettingModel(Setting s) => 
            (
                s == null ? null :
                new SettingModel
                {
                    Id = s.Id,
                    GroupName = s.GroupName,
                    Name = s.Name,
                    Type = s.Type,
                    Value = s.Value,
                    Comment = s.Comment,
                    IsEnvironmentSpecific = s.IsEnvironmentSpecific,
                    LastModified = s.Updated.ToString(),
                    IsMasked = s.IsMasked
                }
            );
    }
}