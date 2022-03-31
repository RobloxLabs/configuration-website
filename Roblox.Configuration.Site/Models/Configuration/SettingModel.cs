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

        public string Group { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public string Comment { get; set; }

        public bool Env { get; set; }

        public string LastModified { get; set; }

        public bool IsMasked { get; set; }

        public bool IsValueSameForAllTestEnvironments { get; set; }

        public bool IsValueUniqueForProduction { get; set; }

        public bool IsShutdownSetting { get; set; }

        public bool IsRestrictedSetting { get; set; }

        // From setting to model
        public static implicit operator SettingModel(Setting s) => 
            (
                s == null ? null :
                new SettingModel
                {
                    Id = s.Id,
                    Group = s.GroupName,
                    Name = s.Name,
                    Type = s.Type,
                    Value = s.Value,
                    Comment = s.Comment,
                    Env = s.IsEnvironmentSpecific,
                    LastModified = s.Updated.ToString(),
                    IsMasked = s.IsMasked
                }
            );

        // From model to setting
        public static implicit operator Setting(SettingModel s) =>
            (
                s == null ? null :
                new Setting
                {
                    Id = s.Id,
                    GroupName = s.Group,
                    Name = s.Name,
                    Type = s.Type,
                    Value = s.Value,
                    Comment = s.Comment,
                    IsEnvironmentSpecific = s.Env,
                    Updated = string.IsNullOrEmpty(s.LastModified) ? DateTime.MinValue : DateTime.Parse(s.LastModified),
                    IsMasked = s.IsMasked
                }
            );

        public SettingModel()
        {
            LastModified = DateTime.Now.ToString();
            Comment = "";
            Env = false;
            IsMasked = false;
            IsValueSameForAllTestEnvironments = false;
            IsValueUniqueForProduction = false;
        }
    }
}