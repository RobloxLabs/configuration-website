using System;

namespace Roblox.Settings.Site.Models.Configuration
{
    public class ConnectionStringModel
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public DateTime Updated { get; set; }
    }
}