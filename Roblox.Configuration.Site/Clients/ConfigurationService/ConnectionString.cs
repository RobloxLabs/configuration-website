using System;
using System.Diagnostics;

namespace Roblox.Configuration.Site.Clients.ConfigurationService
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class ConnectionString : IConnectionString
    {
        /// <inheritdoc/>
        public int Id { get; set; }

        /// <inheritdoc/>
        public string GroupName { get; set; }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public string Value { get; set; }

        /// <inheritdoc/>
        public DateTime Updated { get; set; }

        private string GetDebuggerDisplay()
        {
            return $"{GroupName}.{Name}";
        }
    }
}
