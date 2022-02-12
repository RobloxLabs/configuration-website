using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roblox.Settings.Site.Models.Configuration
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
    }
}