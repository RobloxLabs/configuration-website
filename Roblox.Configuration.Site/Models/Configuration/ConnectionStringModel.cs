using System;
using Roblox.Configuration.Site.Clients.ConfigurationService;

namespace Roblox.Configuration.Site.Models.Configuration
{
    public class ConnectionStringModel : ConnectionString
    {
        public string LastModified { get { return Updated.ToString(); } }

        public ConnectionStringModel()
        {
            Updated = DateTime.MinValue;
        }

        public ConnectionStringModel(ConnectionString cs)
        {
            Id = cs.Id;
            GroupName = cs.GroupName;
            Name = cs.Name;
            Value = cs.Value;
            Updated = cs.Updated;
        }
    }
}